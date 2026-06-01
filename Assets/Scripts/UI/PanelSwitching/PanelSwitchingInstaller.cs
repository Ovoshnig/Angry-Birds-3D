using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PanelSwitchingInstaller : IInstaller
{
    [SerializeField] private RectTransform _buttonsParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<PanelCloseButtonView> closeButtonViews = _buttonsParent
            .GetComponentsInChildren<PanelCloseButtonView>(true);

        builder.RegisterInstance(closeButtonViews);
        builder.RegisterEntryPoint<InputProviderCloseButtonViewsMediator>();
    }
}
