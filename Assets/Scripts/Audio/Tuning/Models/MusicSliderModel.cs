public sealed class MusicSliderModel : AudioSliderModel
{
    public MusicSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
        : base(settingsStorage, audioSettings)
    {
    }

    public override AudioChannel Channel => AudioChannel.Music;
    public override string MixerParameterName => AudioMixerConstants.MusicGroupName;

    protected override string DataKey => SettingsConstants.MusicVolumeKey;
}
