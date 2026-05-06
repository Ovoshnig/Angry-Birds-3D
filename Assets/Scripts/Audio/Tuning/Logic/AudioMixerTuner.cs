using UnityEngine.Audio;

public class AudioMixerTuner
{
    private readonly AudioMixerGroup _audioMixerGroup;
    private readonly AudioSettings _audioSettings;

    public AudioMixerTuner(AudioMixerGroup audioMixerGroup,
        AudioSettings audioSettings)
    {
        _audioMixerGroup = audioMixerGroup;
        _audioSettings = audioSettings;
    }

    private AudioMixer AudioMixer => _audioMixerGroup.audioMixer;

    public bool SetVolume(string parameterName, float value) => AudioMixer.SetFloat(parameterName, value);

    public void SetPause(bool value)
    {
        AudioMixerSnapshot snapshot = AudioMixer.FindSnapshot(value
            ? AudioMixerConstants.PauseSnapshotName
            : AudioMixerConstants.NormalSnapshotName);

        snapshot.TransitionTo(_audioSettings.SnapshotTransitionDuration);
    }
}
