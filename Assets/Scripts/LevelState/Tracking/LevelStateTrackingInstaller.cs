using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    [SerializeField] private ClearingPanelView _clearingPanelView;
    [SerializeField] private FinalScoreView _finalScoreView;
    [SerializeField] private FailurePanelView _failurePanelView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_clearingPanelView);
        builder.RegisterInstance(_finalScoreView);
        builder.RegisterInstance(_failurePanelView);

        builder.Register<LevelStateTracker>(Lifetime.Singleton);

        builder.RegisterEntryPoint<FinalScoreViewScoreModelMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<FailureViewLevelTrackerMediator>(Lifetime.Singleton);
    }
}
