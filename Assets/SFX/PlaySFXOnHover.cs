using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySFXOnHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
