using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    public void OnPause()
    {
        PauseMenu.Instance.Paused = !PauseMenu.Instance.Paused;
    }
}
