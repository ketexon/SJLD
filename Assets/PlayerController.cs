using Kutie;
using UnityEngine;
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

    [System.NonSerialized] public int NJumps = 1;

    int _health = 3;
    public int Health
    {
        get => _health;
        set {
            if(_health != value)
            {
                _health = value;
                UIManager.Instance.SetHearts(Health);
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
        if (!enabled || !v.performed) return;
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

    void TakeDamage()
    {
        Health -= 1;
        ScreenshakeManager.Instance.TemporaryScreenShake(10, 5, 0.1f);
    }
}