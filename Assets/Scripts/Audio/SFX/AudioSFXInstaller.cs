using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class AudioSFXInstaller : IInstaller
{
    [SerializeField] private SFXPlayerView _sfxPlayerViewPrefab;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_sfxPlayerViewPrefab);
        builder.Register<SFXPlayerObjectPool>(Lifetime.Singleton);
    }
}
