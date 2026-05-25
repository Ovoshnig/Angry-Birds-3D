using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PauseMenuWindowInstaller : IInstaller
{
    [SerializeField] private WindowView _pauseMenuWindowView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pauseMenuWindowView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<PauseMenuWindow>().AsSelf().As<Window>();
            entryPoints.Add<WindowMediator>();
        });
    }
}
