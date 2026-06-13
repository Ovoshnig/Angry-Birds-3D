using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SceneLoadingScreenInstaller : IInstaller
{
    [SerializeField] private LoadingScreenView _loadingScreenView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponentInNewPrefab(_loadingScreenView, Lifetime.Singleton)
            .DontDestroyOnLoad();

        builder.RegisterEntryPoint<SceneSwitchLoadingScreenViewMediator>();
    }
}
