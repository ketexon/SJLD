using UnityEngine;

public class CarDoor : MonoBehaviour
{
    [SerializeField] Car car;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Train.Instance.EnterCar(car);
        Destroy(gameObject);
    }
}
