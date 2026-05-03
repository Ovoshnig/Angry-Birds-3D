using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderView : MonoBehaviour
{
    private Slider _slider;

    public Observable<float> ValueChanged { get; private set; }
    public float MinValue => _slider.minValue;
    public float MaxValue => _slider.maxValue;

    protected virtual void Awake()
    {
        _slider = GetComponent<Slider>();
        ValueChanged = _slider.OnValueChangedAsObservable();
    }

    public void SetValue(float value) => _slider.value = value;

    public void SetValueWithoutNotify(float value) => _slider.SetValueWithoutNotify(value);

    public void SetMinValue(float value) => _slider.minValue = value;

    public void SetMaxValue(float value) => _slider.maxValue = value;
}
