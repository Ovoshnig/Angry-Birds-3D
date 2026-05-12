using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    [SerializeField] private CompletionPanelView _completionPanelView;
    [SerializeField] private FinalScoreView _finalScoreView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_completionPanelView);
        builder.RegisterInstance(_finalScoreView);

        builder.Register<LevelStateTracker>(Lifetime.Singleton);

        builder.RegisterEntryPoint<CompletionViewBirdPointsDisplayerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<FinalScoreViewBirdPointsDisplayerMediator>(Lifetime.Singleton);
    }
}
