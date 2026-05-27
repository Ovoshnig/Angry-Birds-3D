using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class RatingInstaller : IInstaller
{
    [SerializeField] private RatingEvaluatorView _evaluatorView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_evaluatorView);
        builder.Register<RatingEvaluator>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<RatingSaver>().AsSelf();
            entryPoints.Add<RatingEvaluatorMediator>();
        });
    }
}
