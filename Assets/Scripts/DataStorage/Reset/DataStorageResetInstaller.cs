using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class DataStorageResetInstaller : IInstaller
{
    [SerializeField] private RectTransform _resetButtonParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<DataResetButtonView> resetButtonViews = _resetButtonParent
            .GetComponentsInChildren<DataResetButtonView>(true);
        builder.RegisterInstance(resetButtonViews);

        builder.RegisterEntryPoint<DataStoragesResetButtonViewsMediator>();
    }
}
