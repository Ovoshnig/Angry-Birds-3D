using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderView : MonoBehaviour
{
    private readonly ReactiveProperty<float> _value = new();

    private Slider _slider = null;

    public ReadOnlyReactiveProperty<float> Value => _value;
    public float MinValue => _slider.minValue;
    public float MaxValue => _slider.maxValue;

    private Slider Slider
    {
        get
        {
            if (_slider == null)
                _slider = GetComponent<Slider>();

            return _slider;
        }
    }

    protected virtual void Awake()
    {
        Slider.OnValueChangedAsObservable()
            .Subscribe(value => _value.Value = value)
            .AddTo(this);
    }

    protected virtual void OnDestroy() => _value.Dispose();

    public void SetValue(float value)
    {
        Slider.value = value;
        _value.Value = value;
    }

    public void SetValueWithoutNotify(float value)
    {
        Slider.SetValueWithoutNotify(value);
        _value.Value = value;
    }

    public void SetMinValue(float value) => Slider.minValue = value;

    public void SetMaxValue(float value) => Slider.maxValue = value;
}
