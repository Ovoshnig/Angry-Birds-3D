using R3;
using System;
using VContainer.Unity;

public abstract class SliderModel : IStartable, IDisposable
{
    private readonly SettingsStorage _settingsStorage;
    private readonly ReactiveProperty<float> _value = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public SliderModel(SettingsStorage settingsStorage) => _settingsStorage = settingsStorage;

    public ReadOnlyReactiveProperty<float> Value => _value;
    public abstract float MinValue { get; }
    public abstract float MaxValue { get; }

    protected abstract string DataKey { get; }
    protected abstract float DefaultValue { get; }

    public virtual void Start()
    {
        float value = _settingsStorage.Get(DataKey, DefaultValue);
        SetClampedValue(value);

        _settingsStorage.ResetHappened
            .Subscribe(_ => _value.Value = DefaultValue)
            .AddTo(_compositeDisposable);
    }

    public virtual void Dispose()
    {
        _settingsStorage.Set(DataKey, _value.Value);

        _compositeDisposable.Dispose();
        _value.Dispose();
    }

    public void SetClampedValue(float value)
    {
        float clampedValue = Math.Clamp(value, MinValue, MaxValue);
        _value.Value = clampedValue;
    }
}
