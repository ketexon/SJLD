using Unity.Cinemachine;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    [SerializeField] new CinemachineCamera camera;
    [SerializeField] CinemachineConfiner2D confiner;
    [SerializeField] CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] bool updateOrthographicSize = false;

    void Start()
    {
        camera.Target.TrackingTarget = PlayerController.Instance.transform;
    }

    void Update()
    {
        noise.AmplitudeGain = ScreenshakeManager.Instance.AmplitudeGain;
        noise.FrequencyGain = ScreenshakeManager.Instance.FrequencyGain;
        if (updateOrthographicSize && confiner)
        {
            var curSize = camera.Lens.OrthographicSize;
            var targetSize = PlayerController.Instance.CameraOrthographicSize;
            if(!Mathf.Approximately(curSize, targetSize))
            {
                camera.Lens.OrthographicSize = targetSize;
                confiner.InvalidateLensCache();
            }
        }
    }
}
