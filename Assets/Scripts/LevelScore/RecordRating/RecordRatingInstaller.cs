using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

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
