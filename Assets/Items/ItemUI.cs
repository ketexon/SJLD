using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] ItemSO item;
    [SerializeField] Image image;
    [SerializeField] public Image Border;
    [SerializeField] public UnityEvent<ItemSO> ItemChangedEvent;

    public ItemSO Item
    {
        get => item;
        set
        {
            if (item != value)
            {
                item = value;
                ItemChangedEvent.Invoke(item);
                image.sprite = Item ? Item.Icon : null;
            }
        }
    }
}
