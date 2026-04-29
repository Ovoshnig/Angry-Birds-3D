using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderView : MonoBehaviour
{
    private Slider _slider = null;

    public Observable<float> ValueChanged => Slider.OnValueChangedAsObservable();
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

    public void SetValue(float value) => Slider.value = value;

    public void SetValueWithoutNotify(float value) => Slider.SetValueWithoutNotify(value);

    public void SetMinValue(float value) => Slider.minValue = value;

    public void SetMaxValue(float value) => Slider.maxValue = value;
}
