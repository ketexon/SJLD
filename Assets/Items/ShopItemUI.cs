using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMPro.TMP_Text priceText;
    [SerializeField] ItemUI itemUI;

    ItemSO itemSO;

    void Awake()
    {
        itemUI.ItemChangedEvent.AddListener(OnItemChanged);
        itemSO = itemUI.Item;
        priceText.text = "";
        UpdateInteractable();

        GameManager.Instance.MartinisChangedEvent.AddListener(OnMartinisChanged);
    }

    void OnMartinisChanged(int v)
    {
        UpdateInteractable();
    }

    void OnItemChanged(ItemSO itemSO)
    {
        this.itemSO = itemSO;
        priceText.text = itemSO.Price.ToString();
        UpdateInteractable();
    }

    void UpdateInteractable()
    {
        if(!GameManager.Instance || !itemUI.Item)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = itemUI ? GameManager.Instance.Martinis >= itemUI.Item.Price : false;
        }
    }

    public void OnButtonClick()
    {
        if (GameManager.Instance.Martinis < itemUI.Item.Price) return;
        ItemManager.Instance.AddItem(itemUI.Item);
        GameManager.Instance.Martinis -= itemUI.Item.Price;
        Destroy(gameObject);
        ToolTip.Instance.Hide();
        MusicManager.Instance.ItemBuySound.Play();
    }
}
