using UnityEngine.Audio;

namespace Ovoshnig.Audio.Tuning;

public class AudioMixerTuner(AudioMixer audioMixer)
{
    private readonly AudioMixer _audioMixer = audioMixer;

    public bool SetVolume(string parameterName, float value) => _audioMixer.SetFloat(parameterName, value);

    public void SetPause(bool isPaused)
    {
        float targetPitch = isPaused ? 0f : 1f;
        _audioMixer.SetFloat(AudioMixerConstants.SFXPitchParameter, targetPitch);
    }
}
