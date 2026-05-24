using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class WindowInstaller : IInstaller
{
    [SerializeField] private WindowView _pauseMenuWindowView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pauseMenuWindowView);

        builder.Register<WindowTracker>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<WindowInputProvider>().AsSelf();
            entryPoints.Add<PauseMenuWindow>().AsSelf().As<Window>();
            entryPoints.Add<WindowMediator>();
        });
    }
}
