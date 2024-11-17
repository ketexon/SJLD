using Unity.Cinemachine;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] new CinemachineCamera camera;
    [SerializeField] CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        camera.Target.TrackingTarget = PlayerController.Instance.transform;
    }

    void Update()
    {
        noise.AmplitudeGain = ScreenshakeManager.Instance.AmplitudeGain;
        noise.FrequencyGain = ScreenshakeManager.Instance.FrequencyGain;
    }
}
