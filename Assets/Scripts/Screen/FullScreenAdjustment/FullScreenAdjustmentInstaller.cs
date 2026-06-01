using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class FullScreenAdjustmentInstaller : IInstaller
{
    [SerializeField] private FullScreenAdjustToggleView _toggleView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_toggleView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<FullScreenAdjuster>().AsSelf();
            entryPoints.Add<FullScreenAdjusterToggleViewMediator>();
        });
    }
}
