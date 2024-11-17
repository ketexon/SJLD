using UnityEngine;

public class GravityItem : MonoBehaviour
{
    [SerializeField] float Angle;

    void Awake()
    {
        Physics2D.gravity = Quaternion.AngleAxis(Angle, Vector3.forward) * Physics.gravity;
    }
}
