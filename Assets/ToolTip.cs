using Kutie;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : SingletonMonoBehaviour<ToolTip>
{
    [SerializeField] Image bkg;
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] float textPadding = 4;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Vector2 offset;

    RectTransform RectTranform => transform as RectTransform;

    Coroutine showCoroutine = null;

    public string Text
    {
        get => text.text;
        set => text.text = value;
    }

    protected override void Awake()
    {
        base.Awake();
        Hide();
    }

    void Start()
    {
        PlayerPoint.Instance.PointEvent.AddListener(OnPoint);
    }

    void OnPoint(Vector2 pos)
    {
        RectTranform.anchoredPosition = pos + offset;
    }

    public void Show(string text)
    {
        canvasGroup.alpha = 0;
        Text = text;
        showCoroutine = this.Defer(() =>
        {
            canvasGroup.alpha = 1;
        });
    }

    public void Hide()
    {
        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        bkg.rectTransform.sizeDelta = new Vector2(text.renderedWidth + 2 * textPadding, text.renderedHeight + 2 * textPadding);
    }
}
