using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class RecordRatingInstaller : IInstaller
{
    [SerializeField] private RecordRatingView _recordRatingView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_recordRatingView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<RecordRatingSaver>().AsSelf();
            entryPoints.Add<RecordRatingViewMediator>();
        });
    }
}
