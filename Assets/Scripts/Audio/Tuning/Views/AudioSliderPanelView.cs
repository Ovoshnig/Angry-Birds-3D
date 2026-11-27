using Ovoshnig.Extensions.UIToolkit;
using Ovoshnig.UI.Basic;
using Ovoshnig.UI.SliderIndication;
using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ovoshnig.Audio.Tuning;

public class AudioSliderPanelView : UIView
{
    [SerializeField] private VisualElementReference<VisualElement> _slidersContainerReference = new();
    [SerializeField] private Sprite[] _fillingSprites;

    private readonly Subject<(AudioChannel Channel, float Value)> _valueChanged = new();
    private readonly CompositeDisposable _sliderDisposables = new();
    private readonly Dictionary<AudioChannel, (AudioSlider Slider, SliderIndicator Indicator)> _blocks = new();

    private bool _isContainerResolved;

    public Observable<(AudioChannel Channel, float Value)> ValueChanged => _valueChanged;

    protected override bool IsVisualReady => _isContainerResolved;

    private void Awake()
    {
        _slidersContainerReference.RegisterReferenceResolvedCallback(OnContainerResolved);
        _slidersContainerReference.RegisterReferenceUnloadedCallback(OnContainerUnloaded);
    }

    protected override void OnDestroy()
    {
        _sliderDisposables.Dispose();
        _valueChanged.Dispose();
        _slidersContainerReference.UnregisterReferenceResolvedCallback(OnContainerResolved);
        _slidersContainerReference.UnregisterReferenceUnloadedCallback(OnContainerUnloaded);
        base.OnDestroy();
    }

    public void SetRange(AudioChannel channel, float minValue, float maxValue)
    {
        if (!_blocks.TryGetValue(channel, out var block))
            return;

        block.Slider.lowValue = minValue;
        block.Slider.highValue = maxValue;
    }

    public void SetValueWithoutNotify(AudioChannel channel, float value)
    {
        if (!_blocks.TryGetValue(channel, out var block))
            return;

        block.Slider.SetValueWithoutNotify(value);
        UpdateIndicator(block.Slider, block.Indicator, value);
    }

    private void OnContainerResolved(VisualElement container)
    {
        _sliderDisposables.Clear();
        _blocks.Clear();

        foreach (VisualElement child in container.Children())
        {
            AudioSlider slider = child.Q<AudioSlider>();

            if (slider == null)
                continue;

            SliderIndicator indicator = child.Q<SliderIndicator>();
            _blocks[slider.Channel] = (slider, indicator);

            slider.ValueChangedAsObservable()
                .Subscribe(value =>
                {
                    UpdateIndicator(slider, indicator, value);
                    _valueChanged.OnNext((slider.Channel, value));
                })
                .AddTo(_sliderDisposables);
        }

        _isContainerResolved = true;
        RefreshEnabledState();
    }

    private void OnContainerUnloaded(VisualElement container)
    {
        _sliderDisposables.Clear();
        _blocks.Clear();
        _isContainerResolved = false;
        RefreshEnabledState();
    }

    private void UpdateIndicator(AudioSlider slider, SliderIndicator indicator, float value)
    {
        if (_fillingSprites.Length <= 1)
            return;

        float range = slider.highValue - slider.lowValue;

        if (range <= 0f)
            return;

        float percentagePerImage = 1f / (_fillingSprites.Length - 1);
        float fillingPercentage = Mathf.Clamp01((value - slider.lowValue) / range);
        int spriteIndex = Mathf.Clamp(
            Mathf.CeilToInt(fillingPercentage / percentagePerImage), 0, _fillingSprites.Length - 1);
        indicator.style.backgroundImage = new StyleBackground(_fillingSprites[spriteIndex]);
    }
}
