using Unity.Cinemachine;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] protected CinemachineCamera vcam;
    [SerializeField] public Transform EndPoint;

    public virtual void OnEnable()
    {
        vcam.enabled = true;
    }

    public virtual void OnDisable()
    {
        vcam.enabled = false;
    }
}
