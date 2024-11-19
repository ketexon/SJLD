using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopCar : Car
{
    [SerializeField] Button goButton;
    [SerializeField] CanvasGroup itemUICanvasGroup;
    [SerializeField] List<ItemUI> itemUIs;
    [SerializeField] Transform dynamicObjectsParent;

    public override void OnEnable()
    {
        base.OnEnable();

        PlayerController.Instance.enabled = false;
        PlayerMovement.Instance.Moving = true;

        goButton.onClick.AddListener(OnGoButtonPressed);

        var buffs = ItemManager.Instance.GetRandomBuffs();
        itemUIs[0].Item = buffs.Item1;
        itemUIs[1].Item = buffs.Item2;
        itemUIs[2].Item = buffs.Item3;

        itemUIs[0].enabled = true;
        itemUIs[1].enabled = true;
        itemUIs[2].enabled = true;

        dynamicObjectsParent.SetParent(null);

        MusicManager.Instance.PlayingFast = false;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        foreach(var itemUI in itemUIs)
        {
            if (itemUI) {
                itemUI.enabled = false;
            }
        }
    }

    void OnGoButtonPressed()
    {
        itemUICanvasGroup.interactable = false;
    }

    public void OnPlayerEnterStandPoint()
    {
        goButton.interactable = true;
        PlayerMovement.Instance.Moving = false;
        itemUICanvasGroup.interactable = true;
    }
}
