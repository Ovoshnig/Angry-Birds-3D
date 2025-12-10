using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ScoreInstaller : IInstaller
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private PointsView _pointsViewPrefab;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_scoreView);
        builder.Register<ScoreModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScoreLogic>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<ScoreMediator>(Lifetime.Singleton);

        builder.RegisterInstance(_pointsViewPrefab);
        builder.Register<PointsObjectPool>(Lifetime.Singleton);
    }
}
