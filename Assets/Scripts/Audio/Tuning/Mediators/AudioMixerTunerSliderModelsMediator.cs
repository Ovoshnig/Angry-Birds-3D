using Ovoshnig.Mediation;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.Audio.Tuning;

public class AudioMixerTunerSliderModelsMediator(AudioMixerTuner audioMixerTuner,
    IReadOnlyList<AudioSliderModel> sliderModels) : Mediator
{
    private readonly AudioMixerTuner _audioMixerTuner = audioMixerTuner;
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels = sliderModels;

    protected override void Bind(CompositeDisposable disposables)
    {
        foreach (var model in _sliderModels)
        {
            model.Value
                .Subscribe(value => _audioMixerTuner.SetVolume(model.MixerParameterName, value))
                .AddTo(disposables);
        }
    }
}
