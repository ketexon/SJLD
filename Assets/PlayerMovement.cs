using Kutie;
using UnityEngine;

public class PlayerMovement : Kutie.Singleton.SingletonMonoBehaviour<PlayerMovement>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float acceleration = 8.0f;
    [SerializeField] float maxSpeed = 2.0f;
    [SerializeField] Animator animator;

    [System.NonSerialized] public float SpeedMult = 1.0f;

    bool _moving = false;
    public bool Moving {
        get => _moving;
        set {
            if(Moving != value)
            {
                _moving = value;
                animator.SetBool("Running", Moving);
            }
        }
    }

    void FixedUpdate()
    {
        if (Moving)
        {
            rb.AddForceX(acceleration * rb.mass);
            rb.linearVelocityX = Mathf.Min(rb.linearVelocityX, maxSpeed * SpeedMult);
        }
        else
        {
            rb.linearVelocityX = 0;
        }
    }
}
