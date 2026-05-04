using R3;
using VContainer.Unity;

public abstract class AudioSliderModel : SliderModel, IStartable
{
    private readonly SettingsStorage _settingsStorage;
    private readonly AudioSettings _audioSettings;

    protected AudioSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
    {
        _settingsStorage = settingsStorage;
        _audioSettings = audioSettings;
    }

    public abstract AudioChannel Channel { get; }
    public abstract string MixerParameterName { get; }

    public override float MinValue => _audioSettings.MinVolume;
    public override float MaxValue => _audioSettings.MaxVolume;

    protected abstract string DataKey { get; }

    protected override float DefaultValue => _audioSettings.DefaultVolume;

    public virtual void Start()
    {
        float value = _settingsStorage.Get(DataKey, DefaultValue);
        SetClampedValue(value);

        _settingsStorage.ResetHappened
            .Subscribe(_ => ResetValue())
            .AddTo(Disposables);
    }

    public override void Dispose()
    {
        _settingsStorage.Set(DataKey, Value.CurrentValue);
        base.Dispose();
    }
}
