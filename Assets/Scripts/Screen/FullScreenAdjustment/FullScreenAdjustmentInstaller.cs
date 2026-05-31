using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class FullScreenAdjustmentInstaller : IInstaller
{
    [SerializeField] private FullScreenAdjusterView _adjusterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_adjusterView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<FullScreenAdjuster>().AsSelf();
            entryPoints.Add<FullScreenAdjusterViewMediator>();
        });
    }
}
