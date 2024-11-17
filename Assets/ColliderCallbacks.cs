using UnityEngine;
using UnityEngine.Events;

public class ColliderCallbacks : MonoBehaviour
{
    [SerializeField] public UnityEvent<Collider2D> TriggerEnter2DEvent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2DEvent.Invoke(collision);
    }
}
