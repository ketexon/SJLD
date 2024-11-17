using Kutie;
using Unity.Cinemachine;
using UnityEngine;

public class ScreenshakeManager : SingletonMonoBehaviour<ScreenshakeManager>
{
    const float LERP_MULT = 10.0f;

    [SerializeField] public float restingAmplitudeGain = 0.03f;
    [SerializeField] public float restingFrequencyGain = 10.0f;

    float tempAmplitudeMult = 1.0f;
    float tempFrequencyMult = 1.0f;

    Coroutine tempGainResetCoro = null;

    public float AmplitudeGain { get; private set; }
    public float FrequencyGain { get; private set; }

    void Update()
    {
        AmplitudeGain = Mathf.Lerp(
            AmplitudeGain,
            restingAmplitudeGain * tempAmplitudeMult,
            Time.deltaTime * LERP_MULT
        );

        FrequencyGain = Mathf.Lerp(
            FrequencyGain,
            restingFrequencyGain * tempFrequencyMult,
            Time.deltaTime * LERP_MULT
        );
    }

    public void TemporaryScreenShake(float amplitudeMult, float frequencyMult, float duration)
    {
        if(tempGainResetCoro != null)
        {
            StopCoroutine(tempGainResetCoro);
        }

        tempAmplitudeMult = amplitudeMult;
        tempFrequencyMult = frequencyMult;

        tempGainResetCoro = this.Defer(() =>
        {
            tempAmplitudeMult = 1.0f;
            tempFrequencyMult = 1.0f;
            tempGainResetCoro = null;
        }, new WaitForSeconds(duration));
    }
}
