using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GoArrow : MonoBehaviour
{
    [FormerlySerializedAs("button")]
    [SerializeField] public Button Button;

    [System.NonSerialized] public bool PreventDefault = false;

    void Start()
    {
        Button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (PreventDefault) return;
        MusicManager.Instance.GoSound.Play();
        Button.interactable = false;
        PlayerMovement.Instance.Moving = true;
    }
}
