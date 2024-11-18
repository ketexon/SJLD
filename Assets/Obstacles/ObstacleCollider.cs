using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    [SerializeField] public bool CanLandOn = false;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected string disabledObstacleLayerName;
    [SerializeField] protected Rigidbody2D rb;

    new protected Collider2D collider;

    virtual protected void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public virtual void Break(Vector2 point, Vector2 dir)
    {
        spriteRenderer.color = new(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            spriteRenderer.color.a * 0.5f
        );

        gameObject.layer = LayerMask.NameToLayer(disabledObstacleLayerName);

        rb.AddForceAtPosition(dir * 8.0f, point, ForceMode2D.Impulse);
    }
}
