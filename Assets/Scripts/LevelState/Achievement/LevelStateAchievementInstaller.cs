using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateAchievementInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<LevelAchiever>().AsSelf();
}
