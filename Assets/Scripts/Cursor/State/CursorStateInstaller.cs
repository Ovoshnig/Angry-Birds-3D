using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class CursorStateInstaller : IInstaller
{
    [SerializeField] private CursorConfiguration _cursorConfiguration;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_cursorConfiguration);
        builder.Register<CursorStateModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<CursorStateSetter>().AsSelf();
    }
}
