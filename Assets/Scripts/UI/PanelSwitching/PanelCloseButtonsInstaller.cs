using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PanelCloseButtonsInstaller : IInstaller
{
    [SerializeField] private RectTransform _closeButtonsParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<PanelCloseButtonView> closeButtonViews = _closeButtonsParent
            .GetComponentsInChildren<PanelCloseButtonView>(true);

        builder.RegisterInstance(closeButtonViews);
        builder.RegisterEntryPoint<UIInputProviderCloseButtonViewMediator>();
    }
}
