using UnityEngine;
using Kutie;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : SingletonMonoBehaviour<MusicManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource deathMusic;

    [SerializeField] float minLowpass;
    [SerializeField] float maxLowpass;
    [SerializeField] float hurtLowpassDuration;
    [SerializeField] AnimationCurve hurtLowpassCurve;

    [SerializeField] public AudioSource ItemBuySound;
    [SerializeField] public AudioSource GoSound;
    [SerializeField] public AudioSource HurtSound;
    [SerializeField] public AudioSource MartiniSound;
    [SerializeField] public AudioSource MissileWarningSound;

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

    public void PlayItemBuySound()
    {
        ItemBuySound.Play();
    }

    public void OnHurt()
    {
        HurtSound.Play();
        StartCoroutine(HurtCoroutine());
    }

    IEnumerator HurtCoroutine()
    {
        float startTime = Time.time;
        float t;
        while ((t = (Time.time - startTime)/hurtLowpassDuration) < 1)
        {
            var value = hurtLowpassCurve.Evaluate(t) * (minLowpass - maxLowpass) + maxLowpass;
            audioMixer.SetFloat("MusicLowpass", value);
            yield return null;
        }
        audioMixer.SetFloat("MusicLowpass", maxLowpass);
    }
}
