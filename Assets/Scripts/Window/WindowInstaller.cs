using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class WindowInstaller : IInstaller
{
    [SerializeField] private PauseMenuWindowInstaller _pauseMenuWindowInstaller;
    [SerializeField] private WindowResumptionInstaller _windowResumptionInstaller;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<WindowInputProvider>().AsSelf();
        builder.Register<WindowTracker>(Lifetime.Singleton);

        _pauseMenuWindowInstaller.Install(builder);
        _windowResumptionInstaller.Install(builder);
    }
}
