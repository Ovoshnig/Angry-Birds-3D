using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

[Serializable]
public class AudioTuningInstaller : IInstaller
{
    [SerializeField] private RectTransform _sliderViewsParent;
    [SerializeField] private AudioMixer _audioMixer;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<AudioSliderView> sliderViews = _sliderViewsParent
            .GetComponentsInChildren<AudioSliderView>(true);

        builder.RegisterInstance(sliderViews);
        builder.RegisterInstance(_audioMixer);

        builder.Register<AudioMixerTuner>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<MusicSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<SFXSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<AudioSliderMediator>();
            entryPoints.Add<MixerTunerSliderModelMediator>();
        });
    }
}
