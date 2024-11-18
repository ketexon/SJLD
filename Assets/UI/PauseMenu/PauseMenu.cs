using UnityEngine;
using Kutie;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : SingletonMonoBehaviour<PauseMenu>
{
    [SerializeField] Canvas canvas;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioMixerSnapshot defaultSnapshot;
    [SerializeField] AudioMixerSnapshot pauseSnapshot;

    bool _paused = false;
    public bool Paused
    {
        get => _paused;
        set
        {
            if(_paused != value)
            {
                _paused = value;
                if (Paused)
                {
                    Pause();
                }
                else
                {
                    UnPause();
                }
            }
        }
    }

    float musicVolume;
    float sfxVolume;

    void Start()
    {
        mixer.GetFloat("MusicVolume", out musicVolume);
        mixer.GetFloat("SFXVolume", out sfxVolume);

        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", musicVolume);

        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;

        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChange);
    }

    void OnMusicVolumeChange(float newVal)
    {
        musicVolume = newVal;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    void OnSfxVolumeChange(float newVal)
    {
        sfxVolume = newVal;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        canvas.enabled = true;
        pauseSnapshot.TransitionTo(0);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
        defaultSnapshot.TransitionTo(0);
    }

    void Update()
    {
        mixer.SetFloat("MusicVolume", musicVolume);
        mixer.SetFloat("SFXVolume", sfxVolume);
    }
}
