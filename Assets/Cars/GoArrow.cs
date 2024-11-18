using UnityEngine;
using UnityEngine.UI;

public class GoArrow : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        MusicManager.Instance.GoSound.Play();
        button.interactable = false;
        PlayerMovement.Instance.Moving = true;
    }
}
