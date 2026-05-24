using UnityEngine.Audio;

public class AudioMixerTuner
{
    private readonly AudioMixer _audioMixer;
    private readonly AudioSettings _audioSettings;

    public AudioMixerTuner(AudioMixer audioMixer,
        AudioSettings audioSettings)
    {
        _audioMixer = audioMixer;
        _audioSettings = audioSettings;
    }

    public bool SetVolume(string parameterName, float value) => _audioMixer.SetFloat(parameterName, value);

    public void SetPause(bool value)
    {
        AudioMixerSnapshot snapshot = _audioMixer.FindSnapshot(value
            ? AudioMixerConstants.PauseSnapshotName
            : AudioMixerConstants.NormalSnapshotName);

        snapshot.TransitionTo(_audioSettings.SnapshotTransitionDuration);
    }
}
