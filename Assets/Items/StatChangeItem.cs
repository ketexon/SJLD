using UnityEngine;

public class StatChangeItem : MonoBehaviour
{
    [SerializeField] float speedMult = 1.0f;
    [SerializeField] int healthDelta = 0;
    [SerializeField] int jumpsDelta = 0;

    void Start()
    {
        PlayerMovement.Instance.SpeedMult *= speedMult;
        PlayerController.Instance.Health += healthDelta;
        PlayerController.Instance.NJumps += jumpsDelta;
    }
}
