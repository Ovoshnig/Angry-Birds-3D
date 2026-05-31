using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class DataStorageResetInstaller : IInstaller
{
    [SerializeField] private RectTransform _resetterViewParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<DataResetterView> resetterViews = _resetterViewParent
            .GetComponentsInChildren<DataResetterView>(true);
        builder.RegisterInstance(resetterViews);

        builder.RegisterEntryPoint<DataStoragesResetterViewsMediator>();
    }
}
