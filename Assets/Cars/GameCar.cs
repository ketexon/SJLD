using UnityEngine;
using System.Collections.Generic;

public class GameCar : Car
{
    [SerializeField] Transform maxSpawnPoint;

    Transform SpawnPoint => ObstacleSpawner.Instance.SpawnPoint.transform;
    public override void OnEnable()
    {
        base.OnEnable();

        ObstacleSpawner.Instance.ClearObstacles();
        ObstacleSpawner.Instance.enabled = true;
        PlayerController.Instance.enabled = true;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (ObstacleSpawner.Instance)
        {
            ObstacleSpawner.Instance.enabled = false;
        }
        if(PlayerController.Instance)
        {
            PlayerController.Instance.enabled = false;
        }
    }

    void Update()
    {
        if(SpawnPoint.position.x > maxSpawnPoint.position.x)
        {
            ObstacleSpawner.Instance.enabled = false;
        }
    }
}
