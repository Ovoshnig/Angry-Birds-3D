using R3;
using System.Collections.Generic;

public class MixerTunerSliderModelMediator : Mediator
{
    private readonly AudioMixerTuner _audioMixerTuner;
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels;

    public MixerTunerSliderModelMediator(AudioMixerTuner audioMixerTuner, IReadOnlyList<AudioSliderModel> sliderModels)
    {
        _audioMixerTuner = audioMixerTuner;
        _sliderModels = sliderModels;
    }

    public override void Start()
    {
        foreach (var model in _sliderModels)
        {
            model.Value
                .Subscribe(value => _audioMixerTuner.SetVolume(model.MixerParameterName, value))
                .AddTo(Disposables);
        }
    }
}
