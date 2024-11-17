using Kutie;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
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
            }
        }
    }
}
