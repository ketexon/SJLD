using UnityEngine;

public class MissileIndicatorManager : Kutie.Singleton.SingletonMonoBehaviour<MissileIndicatorManager>
{
    [SerializeField] GameObject missileIndicatorPrefab;

    public MissileIndicator Add(Transform spawnPoint, float delay)
    {
        //target = spawnPoint;
        var missileIndicatorGO = Instantiate(missileIndicatorPrefab, transform);
        (missileIndicatorGO.transform as RectTransform).anchoredPosition = Vector2.zero;
        var missileIndicator = missileIndicatorGO.GetComponent<MissileIndicator>();
        missileIndicator.Target = spawnPoint;
        missileIndicator.Delay = delay;
        return missileIndicator;
    }
}
