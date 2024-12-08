using UnityEngine;
using System.Collections.Generic;

public class Background : Kutie.Singleton.SingletonMonoBehaviour<Background>
{
    [SerializeField] Transform target;
    [SerializeField] List<Transform> layers;
    [SerializeField] List<float> speeds;
    [SerializeField] float repeatX;

    void Start()
    {
        Debug.Assert(speeds.Count == layers.Count);
    }

    void Update()
    {
        for(int i = 0; i < layers.Count; ++i)
        {
            var layer = layers[i];
            var speed = speeds[i];
            layer.transform.localPosition += speed * Time.deltaTime * Vector3.right;
            var targetPositionLocal = transform.InverseTransformPoint(target.position);
            layer.transform.localPosition = new(
                (layer.transform.localPosition.x - targetPositionLocal.x) % repeatX + targetPositionLocal.x,
                layer.transform.localPosition.y,
                layer.transform.localPosition.z
            );
        }
    }
}
