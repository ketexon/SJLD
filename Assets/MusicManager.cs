using UnityEngine;
using Kutie;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : SingletonMonoBehaviour<MusicManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource bgmSlow;
    [SerializeField] AudioSource bgmFast;
    [SerializeField] float bgmLerpMult = 1.0f;
    [SerializeField] AudioSource deathMusic;
    [SerializeField] AudioMixerSnapshot defaultSnapshot;

    [SerializeField] float minLowpass;
    [SerializeField] float maxLowpass;
    [SerializeField] float hurtLowpassDuration;
    [SerializeField] AnimationCurve hurtLowpassCurve;

    [SerializeField] public AudioSource ItemBuySound;
    [SerializeField] public AudioSource GoSound;
    [SerializeField] public AudioSource HurtSound;
    [SerializeField] public AudioSource MartiniSound;

    float bgmFastVolume;
    float bgmSlowVolume;
    [System.NonSerialized] public bool PlayingFast = true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        bgmFastVolume = bgmFast.volume;
        bgmSlowVolume = bgmSlow.volume;

        bgmFast.Play();
        bgmSlow.Play();
        bgmSlow.volume = 0.0f;

        deathMusic.Stop();

        GameManager.Instance.RestartEvent.AddListener(OnRestart);
        GameManager.NewInstanceEvent.AddListener(OnNewGameManagerInstance);
    }

    void Update()
    {
        bgmFast.volume = Mathf.Lerp(
            bgmFast.volume,
            PlayingFast ? bgmFastVolume : 0.0f,
            Time.deltaTime * bgmLerpMult
        );

        bgmSlow.volume = Mathf.Lerp(
            bgmSlow.volume,
            !PlayingFast ? bgmSlowVolume : 0.0f,
            Time.deltaTime * bgmLerpMult
        );
    }

    void OnNewGameManagerInstance(GameManager gm)
    {
        gm.RestartEvent.AddListener(OnRestart);
    }

    void OnRestart()
    {
        if (deathMusic.isPlaying)
        {
            bgmFast.Play();
            bgmFast.volume = bgmFastVolume;
            bgmSlow.Play();
            bgmSlow.volume = 0;

            deathMusic.Stop();
        }
        defaultSnapshot.TransitionTo(0);
    }

    public void OnDeath()
    {
        bgmSlow.Stop();
        bgmFast.Stop();
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
