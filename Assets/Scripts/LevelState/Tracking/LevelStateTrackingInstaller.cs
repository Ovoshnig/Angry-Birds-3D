using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    [SerializeField] private ClearingPanelView _clearingPanelView;
    [SerializeField] private FailurePanelView _failurePanelView;
    [SerializeField] private FinalScoreView _finalScoreView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_clearingPanelView);
        builder.RegisterInstance(_failurePanelView);
        builder.RegisterInstance(_finalScoreView);

        builder.Register<LevelStateTracker>(Lifetime.Singleton);

        builder.RegisterEntryPoint<FinalScoreViewComplitionPanelMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ClearingViewBirdPointsDisplayerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<FailureViewLevelTrackerMediator>(Lifetime.Singleton);
    }
}
