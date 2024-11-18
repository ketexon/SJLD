using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squirmer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] List<AudioClip> clips;
    [SerializeField] float playInterval;
    [SerializeField] float fadeOutDuration;
    [SerializeField] float leftPitch = 1.0f;
    [SerializeField] float rightPitch = 1.3f;

    [System.NonSerialized] public bool Left = true;

    float sourceVolume;

    Coroutine coro;

    void Start()
    {
        sourceVolume = source.volume;
        source.pitch = Left ? leftPitch : rightPitch;
    }

    public void StartAudio()
    {
        if (coro != null) StopCoroutine(coro);
        coro = StartCoroutine(PlayCoroutine());
    }

    public void StopAudio()
    {
        if(coro != null) StopCoroutine(coro);
        if (isActiveAndEnabled)
        {
            coro = StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator PlayCoroutine()
    {
        while (true)
        {
            var randomClip = clips[Random.Range(0, clips.Count)];
            source.volume = sourceVolume;
            source.PlayOneShot(randomClip);
            yield return new WaitForSeconds(randomClip.length + playInterval);
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        if (source.isPlaying)
        {
            float startTime = Time.time;
            float t;
            while((t = (Time.time - startTime)/fadeOutDuration) < 1)
            {
                yield return null;
                source.volume = Mathf.Lerp(sourceVolume, 0, t);
            }
        }
        source.Stop();
    }
}
