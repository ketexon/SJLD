using Kutie;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerPoint : SingletonMonoBehaviour<PlayerPoint>
{
    public UnityEvent<Vector2> PointEvent;
    public UnityEvent ClickEvent;

    public void OnPoint(InputAction.CallbackContext v)
    {
        PointEvent.Invoke(v.ReadValue<Vector2>());
    }

    public void OnClick(InputAction.CallbackContext v)
    {
        if (v.performed)
        {
            ClickEvent.Invoke();
        }
    }
}
