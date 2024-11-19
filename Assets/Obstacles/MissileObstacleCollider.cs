using UnityEngine;
using System.Collections.Generic;
using Kutie;

public class MissileObstacleCollider : ObstacleCollider
{
    [SerializeField] List<AudioClip> meowClips;
    [SerializeField] AudioSource source;
    [SerializeField] float clipPlayDelay = 0.25f;

    void Start()
    {
        this.Defer(() =>
        {
            source.PlayOneShot(meowClips[Random.Range(0, meowClips.Count)]);
        }, new WaitForSeconds(clipPlayDelay));
    }

    public override void Break(Vector2 point, Vector2 dir)
    {
        base.Break(point, dir);
        rb.gravityScale = 1.0f;
    }
}
