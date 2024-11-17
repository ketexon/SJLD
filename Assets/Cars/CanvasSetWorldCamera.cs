using UnityEngine;

public class CanvasSetWorldCamera : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
