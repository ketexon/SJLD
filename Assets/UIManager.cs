using Kutie;
using UnityEngine;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] Transform heartsContainer;
    [SerializeField] GameObject heartPrefab;

    [SerializeField] TMPro.TMP_Text martinisText;
    [SerializeField] TMPro.TMP_Text scoreText;

    int CurHearts => heartsContainer.childCount;

    override protected void Awake()
    {
        base.Awake();
    }

    public void SetHearts(int nHearts)
    {
        int delta = nHearts - CurHearts;
        for(; delta < 0; delta++)
        {
            Destroy(heartsContainer.GetChild(CurHearts - 1).gameObject);
        }
        for (; delta > 0; delta--)
        {
            Instantiate(heartPrefab, heartsContainer);
        }
    }

    public void SetMartinis(int nMartinis)
    {
        martinisText.text = nMartinis.ToString();
    }

    public void SetScore(string text)
    {
        scoreText.text = text;
    }
}
