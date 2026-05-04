using R3;
using System;

public abstract class SliderModel : IDisposable
{
    private readonly ReactiveProperty<float> _value = new();

    public ReadOnlyReactiveProperty<float> Value => _value;
    public abstract float MinValue { get; }
    public abstract float MaxValue { get; }

    protected CompositeDisposable Disposables { get; } = new();
    protected abstract float DefaultValue { get; }

    public virtual void Dispose()
    {
        Disposables.Dispose();
        _value.Dispose();
    }

    public void SetClampedValue(float value)
    {
        float clampedValue = Math.Clamp(value, MinValue, MaxValue);
        _value.Value = clampedValue;
    }

    protected void ResetValue() => _value.Value = DefaultValue;
}
