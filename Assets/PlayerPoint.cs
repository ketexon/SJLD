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
        if (PauseMenu.Instance && PauseMenu.Instance.Paused) return;
        PointEvent.Invoke(v.ReadValue<Vector2>());
    }

    public void OnClick(InputAction.CallbackContext v)
    {
        if (PauseMenu.Instance.Paused) return;
        if (v.performed)
        {
            ClickEvent.Invoke();
        }
    }
}
