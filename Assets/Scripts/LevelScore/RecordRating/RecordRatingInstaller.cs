using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.LevelScore.RecordRating
{
    [Serializable]
    public class RecordRatingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<RecordRatingView>();

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<RecordRatingSaver>().AsSelf();
                entryPoints.Add<RecordRatingSaverViewMediator>();
            });
        }
    }
}
