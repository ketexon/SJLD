using UnityEngine;
using Kutie;

public class MusicManager : SingletonMonoBehaviour<MusicManager>
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource deathMusic;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        bgm.Play();
        deathMusic.Stop();

        GameManager.Instance.RestartEvent.AddListener(OnRestart);
    }

    void OnRestart()
    {
        GameManager.Instance.RestartEvent.AddListener(OnRestart);
        if (deathMusic.isPlaying)
        {
            bgm.Play();
            deathMusic.Stop();
        }
    }

    public void OnDeath()
    {
        bgm.Stop();
        deathMusic.Play();
    }
}
