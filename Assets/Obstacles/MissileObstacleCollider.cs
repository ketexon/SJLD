using UnityEngine;

public class MissileObstacleCollider : ObstacleCollider
{
    public override void Break(Vector2 point, Vector2 dir)
    {
        base.Break(point, dir);
        rb.gravityScale = 1.0f;
    }
}
