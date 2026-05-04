public sealed class SFXSliderModel : AudioSliderModel
{
    public SFXSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
        : base(settingsStorage, audioSettings)
    {
    }

    protected override string DataKey => SettingsConstants.SFXVolumeKey;
}
