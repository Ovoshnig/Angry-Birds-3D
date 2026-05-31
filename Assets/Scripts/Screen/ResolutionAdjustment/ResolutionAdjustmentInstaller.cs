using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ResolutionAdjustmentInstaller : IInstaller
{
    [SerializeField] private ResolutionAdjusterView _adjusterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_adjusterView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<ResolutionAdjuster>().AsSelf();
            entryPoints.Add<ResolutionAdjusterViewMediator>();
        });
    }
}
