using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.LevelState.Achievement
{
    [Serializable]
    public class LevelStateAchievementInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<LevelAchiever>().AsSelf();
    }
}
