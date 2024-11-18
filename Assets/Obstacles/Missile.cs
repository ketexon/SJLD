using UnityEngine;

public class Missile : Obstacle
{
    [SerializeField] float minHeight = 0.2f;
    [SerializeField] float maxHeight = 4.0f;
    [SerializeField] float delay = 2.0f;
    [SerializeField] GameObject missileObjectPrefab;

    float height;

    GameObject missileObjectGO = null;

    void Awake()
    {
        height = Random.Range(minHeight, maxHeight);
        var missileIndicator = MissileIndicatorManager.Instance.Add(transform, delay);
        missileIndicator.FireEvent.AddListener(Fire);
    }

    void Fire()
    {
        missileObjectGO = Instantiate(missileObjectPrefab, transform.position, Quaternion.identity);
    }

    void OnDestroy()
    {
        if (missileObjectGO)
        {
            Destroy(missileObjectGO);
        }
    }

    void Update()
    {
        transform.position = SpawnPoint.transform.position + Vector3.up * height;
    }
}
