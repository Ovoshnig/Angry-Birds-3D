using System;
using UnityEngine;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class RatingEvaluationInstaller : IInstaller
{
    [SerializeField] private RatingSettings _ratingSettings;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_ratingSettings);
        builder.RegisterInstanceInHierarchy<RatingEvaluatorView>();

        builder.Register<RatingEvaluator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<RatingEvaluatorViewMediator>();
    }
}
