using System;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class AudioTuningInstaller : IInstaller
{
    [SerializeField] private AudioMixer _audioMixer;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_audioMixer);
        builder.RegisterInstancesInHierarchy<AudioSliderView>();

        builder.Register<AudioMixerTuner>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<MusicSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<SFXSliderModel>().As<AudioSliderModel>().AsSelf();
            entryPoints.Add<AudioSliderModelsSliderViewsMediator>();
            entryPoints.Add<AudioMixerTunerSliderModelsMediator>();
        });
    }
}
