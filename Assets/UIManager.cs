using Kutie;
using UnityEngine;

public class UIManager : Kutie.Singleton.SingletonMonoBehaviour<UIManager>
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
        if(nHearts > CurHearts)
        {
            for(int i = CurHearts; i < nHearts; ++i)
            {
                Instantiate(heartPrefab, heartsContainer);
            }
        }
        else if(nHearts < CurHearts) { }
        {
            for(int i = CurHearts - 1; i >= nHearts; --i)
            {
                Destroy(heartsContainer.GetChild(i).gameObject);
            }
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
