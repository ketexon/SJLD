using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ItemUI itemUI;
    [Multiline]
    [SerializeField] string format;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var text = string.Format(
            format,
            itemUI.Item.Name,
            itemUI.Item.Description
        );
        ToolTip.Instance.Show(text);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.Hide();
    }
}
