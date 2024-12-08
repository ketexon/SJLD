using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : Kutie.Singleton.SingletonMonoBehaviour<GameManager>
{
    [SerializeField] string scoreTemplateText = "<color=#4464D1>{0}</color> dilemma{1} solved";

    public string ScoreText => string.Format(scoreTemplateText, Score, Score == 1 ? "" : "s");

    public UnityEvent RestartEvent;
    public UnityEvent<int> MartinisChangedEvent;

    bool restarting = false;

    [SerializeField] int _martinis = 0;
    public int Martinis
    {
        get => _martinis;
        set
        {
            if (Martinis != value)
            {
                _martinis = value;
                UIManager.Instance.SetMartinis(value);
                MartinisChangedEvent.Invoke(value);
            }
        }
    }

    [SerializeField] int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            if (Score != value)
            {
                _score = value;
                UIManager.Instance.SetScore(ScoreText);
            }
        }
    }

    [System.NonSerialized] public int AxolotlsKilled = 0;
    [System.NonSerialized] public int AxolotlsSaved = 0;
    [System.NonSerialized] public int AxolotlsDeltaKilled = 0;
    [System.NonSerialized] public int AxolotlsDeltaSaved = 0;
    [System.NonSerialized] public int BadDecisions = 0;
    [System.NonSerialized] public int GoodDecisions = 0;
    [System.NonSerialized] public bool CamusEnding = false;

    override protected void Awake()
    {
        base.Awake();
    }

    public void RestartGame()
    {
        if(restarting) return;
        restarting = true;
        Time.timeScale = 1.0f;
        RestartEvent.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
