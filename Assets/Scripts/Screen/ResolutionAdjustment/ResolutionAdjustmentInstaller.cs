using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ResolutionAdjustmentInstaller : IInstaller
{
    [SerializeField] private ResolutionAdjustDropdownView _dropdownView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_dropdownView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<ResolutionAdjuster>().AsSelf();
            entryPoints.Add<ResolutionAdjusterDropdownViewMediator>();
        });
    }
}
