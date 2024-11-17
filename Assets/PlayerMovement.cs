using Kutie;
using UnityEngine;

public class PlayerMovement : SingletonMonoBehaviour<PlayerMovement>
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float acceleration = 8.0f;
    [SerializeField] float maxSpeed = 2.0f;

    [System.NonSerialized] public float SpeedMult = 1.0f;

    public bool Moving { get; set; } = false;

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
