using UnityEngine;

public class SetVelocityOnStart : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = velocity;
    }
}