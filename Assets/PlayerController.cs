using Kutie;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] Transform foot;
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] LayerMask obstacleLayerMask;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpVelocity = 5.0f;
    [SerializeField] float groundedRaycastDistance = 0.2f;
    [SerializeField] float jumpCooldown = 0.2f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] public UnityEvent DeathEvent;
    [SerializeField] Animator animator;
    [SerializeField] CinemachineCamera deathCamera;
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] public float CameraOrthographicSize = 2.5f;

    [System.NonSerialized] public int NJumps = 1;

    public bool Dead => Health <= 0;

    int _health = 3;
    public int Health
    {
        get => _health;
        set {
            if(Health != value)
            {
                _health = value;
                UIManager.Instance.SetHearts(Health);
                if(Health <= 0)
                {
                    Die();
                }
            }
        }
    }

    float lastJumpTime = float.NegativeInfinity;
    bool isGrounded = false;
    int jumpsRemaining = 1;

    void Start()
    {
        UIManager.Instance.SetHearts(Health);
    }

    void OnEnable()
    {
        lastJumpTime = float.NegativeInfinity;
        isGrounded = false;
        playerMovement.Moving = true;
    }

    public void Jump(InputAction.CallbackContext v)
    {
        if (!enabled || !v.performed || Dead) return;
        if (PauseMenu.Instance.Paused) return;
        if (Time.time > lastJumpTime + jumpCooldown && jumpsRemaining > 0)
        {
            lastJumpTime = Time.time;
            rb.linearVelocityY = jumpVelocity;
            jumpsRemaining--;
        }
    }

    void Update()
    {
        UpdateIsGrounded();
        if(isGrounded && Time.time > lastJumpTime + jumpCooldown)
        {
            jumpsRemaining = NJumps;
        }
    }

    void UpdateIsGrounded()
    {
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.Raycast(
            foot.transform.position,
            Vector2.down,
            groundedRaycastDistance,
            floorLayerMask
        );
        if (hit)
        {
            if (hit.collider.GetComponent<ObstacleCollider>() is ObstacleCollider obstacle)
            {
                isGrounded = obstacle.CanLandOn;
            }
            else
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
        animator.SetBool("InAir", !isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var layer = collision.collider.gameObject.layer;
        if (obstacleLayerMask.Contains(layer))
        {
            if(collision.collider.GetComponent<ObstacleCollider>() is ObstacleCollider obstacle)
            {
                bool hit = true;

                var contact = collision.GetContact(0);
                if (obstacle.CanLandOn)
                {
                    var angle = Vector2.Angle(Vector2.left, contact.normal);
                    // if we can land on the obstacle, check if we hit it head on
                    // by checking if any of the contacts have a normal going left
                    if (angle > 45)
                    {
                        hit = false;
                    }
                }

                if (hit)
                {
                    obstacle.Break(contact.point, -contact.normal);
                    TakeDamage();
                }
            }
        }
    }

    void Die()
    {
        DeathEvent.Invoke();
        PlayerMovement.Instance.Moving = false;
        MusicManager.Instance.OnDeath();
        animator.SetBool("Dead", true);
        deathCamera.enabled = true;

        this.Defer(() =>
        {
            DeathUI.Instance.Show();
        }, new WaitForSeconds(4f));
    }

    void TakeDamage()
    {
        if (!Dead)
        {
            Health -= 1;
            ScreenshakeManager.Instance.TemporaryScreenShake(10, 5, 0.1f);
        }
    }
}