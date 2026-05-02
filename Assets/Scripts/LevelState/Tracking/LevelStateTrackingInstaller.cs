using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    [SerializeField] private CompletionPanelView _completionPanelView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_completionPanelView);
        builder.Register<LevelStateTracker>(Lifetime.Singleton);
        builder.RegisterEntryPoint<CompletionViewLevelTrackerMediator>(Lifetime.Singleton);
    }
}
