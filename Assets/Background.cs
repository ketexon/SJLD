using UnityEngine;
using System.Collections.Generic;

public class Background : MonoBehaviour
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
            layer.transform.position += speed * Time.deltaTime * Vector3.right;
            layer.transform.position = new(
                (layer.transform.position.x - target.position.x) % repeatX + target.position.x,
                layer.transform.position.y,
                layer.transform.position.z
            );
        }
    }
}
