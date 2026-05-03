using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineBasicMultiChannelPerlin))]
public class CameraNoiseView : MonoBehaviour
{
    [SerializeField] private float _noiseAmplitude = 1f;
    [SerializeField] private float _noiseFrequency = 1f;

    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    private void Awake() => _multiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();

    private void Start() => SetNormal();

    public void SetNormal()
    {
        _multiChannelPerlin.AmplitudeGain = 0f;
        _multiChannelPerlin.FrequencyGain = 0f;
    }

    public void SetNoise()
    {
        _multiChannelPerlin.AmplitudeGain = _noiseAmplitude;
        _multiChannelPerlin.FrequencyGain = _noiseFrequency;
    }
}
