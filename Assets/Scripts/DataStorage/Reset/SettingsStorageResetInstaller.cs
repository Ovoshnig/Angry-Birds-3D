using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SettingsStorageResetInstaller : IInstaller
{
    [SerializeField] private SettingsResetButtonView _resetButtonView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_resetButtonView);
        builder.RegisterEntryPoint<SettingsStorageResetViewMediator>();
    }
}
