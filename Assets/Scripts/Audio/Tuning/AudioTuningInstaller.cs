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

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<MusicSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<SFXSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<AudioSliderMediator>();
            entryPoints.Add<MixerTunerSliderModelMediator>();
        });
    }
}
