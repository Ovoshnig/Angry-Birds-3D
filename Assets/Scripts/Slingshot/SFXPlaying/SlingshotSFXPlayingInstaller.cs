using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotSFXPlayingInstaller : IInstaller
{
    [SerializeField] private SlingshotSFXPlayerView _slingshotSFXPlayerView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_slingshotSFXPlayerView);
        builder.RegisterEntryPoint<SlingshotSFXPlayerMediator>(Lifetime.Singleton);
    }
}
