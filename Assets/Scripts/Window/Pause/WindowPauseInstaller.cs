using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class WindowPauseInstaller : IInstaller
{
    [SerializeField] private PauseButtonView _pauseButtonView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pauseButtonView);
        builder.RegisterEntryPoint<PauseMenuWindowButtonViewMediator>();
    }
}
