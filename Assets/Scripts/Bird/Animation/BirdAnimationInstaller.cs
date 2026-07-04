using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdAnimationInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<WhiteBirdAnimatorViewPowerActivatorMediator>();
}
