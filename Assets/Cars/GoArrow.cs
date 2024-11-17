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
        button.interactable = false;
        PlayerMovement.Instance.Moving = true;
    }
}
