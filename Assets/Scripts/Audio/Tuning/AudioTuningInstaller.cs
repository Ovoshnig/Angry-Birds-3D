using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class AudioTuningInstaller : IInstaller
{
    [SerializeField] private RectTransform _sliderViewsParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<SliderView> sliderViews = _sliderViewsParent.GetComponentsInChildren<SliderView>(true);
        builder.RegisterInstance(sliderViews);

        builder.Register<AudioMixerTuner>(Lifetime.Singleton);

        builder.RegisterEntryPoint<MusicSliderModel>(Lifetime.Singleton)
            .As<AudioSliderModel>()
            .AsSelf();

        builder.RegisterEntryPoint<SFXSliderModel>(Lifetime.Singleton)
            .As<AudioSliderModel>()
            .AsSelf();

        builder.RegisterEntryPoint<AudioSliderMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<MixerTunerSliderModelMediator>(Lifetime.Singleton);
    }
}
