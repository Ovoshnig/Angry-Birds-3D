using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class RatingEvaluationInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<RatingEvaluatorView>();
        builder.Register<RatingEvaluator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<RatingEvaluatorViewMediator>();
    }
}
