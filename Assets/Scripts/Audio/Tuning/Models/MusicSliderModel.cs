public sealed class MusicSliderModel : AudioSliderModel
{
    public MusicSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
        : base(settingsStorage, audioSettings)
    {
    }

    protected override string DataKey => SettingsConstants.MusicVolumeKey;
}
