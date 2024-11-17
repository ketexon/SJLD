using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Kutie;

public class ObstacleSpawner : SingletonMonoBehaviour<ObstacleSpawner>
{
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] float minObstacleInterval = 3.0f;
    [SerializeField] float maxObstacleInterval = 5.0f;
    [SerializeField] public Transform SpawnPoint;

    List<Obstacle> obstacles = new();

    Coroutine spawnCoroutine;

    void OnEnable()
    {
        spawnCoroutine = StartCoroutine(SpawnObstacles());
    }

    void OnDisable()
    {
        if(spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(Random.Range(minObstacleInterval, maxObstacleInterval));
        }
    }

    void SpawnObstacle()
    {
        var obstaclePrefab = prefabs[Random.Range(0, prefabs.Count)];
        var obstacleGO = Instantiate(obstaclePrefab);
        obstacleGO.transform.SetParent(transform);
        obstacleGO.transform.position = SpawnPoint.position;

        var obstacle = obstacleGO.GetComponent<Obstacle>();
        obstacle.SpawnPoint = SpawnPoint;

        obstacles.Add(obstacle);
    }

    public void ClearObstacles()
    {
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        obstacles.Clear();
    }
}