using Kutie;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissileIndicator : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] AudioSource audioSource;

    [System.NonSerialized] public Transform Target;
    [System.NonSerialized] public float Delay;
    [System.NonSerialized] public UnityEvent FireEvent = new();

    new Camera camera;

    RectTransform RectTransform => (transform as RectTransform);

    void Start()
    {
        camera = Camera.main;

        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        image.enabled = true;
        for(int i = 0; i < 5; ++i)
        {
            if (image.enabled)
            {
                audioSource.Play();
            }
            yield return new WaitForSeconds(Delay / 5);
            image.enabled = !image.enabled;
        }
        FireEvent.Invoke();
        Destroy(gameObject);
    }

    void Update()
    {
        if (Target)
        {
            var screenPoint = camera.WorldToScreenPoint(Target.position);
            RectTransform.anchoredPosition = RectTransform.anchoredPosition.WithY(screenPoint.y);
        }
    }
}
