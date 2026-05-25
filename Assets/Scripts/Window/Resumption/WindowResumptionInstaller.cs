using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class WindowResumptionInstaller : IInstaller
{
    [SerializeField] private ResumeButtonView _resumeButtonView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_resumeButtonView);
        builder.RegisterEntryPoint<WindowResumeButtonViewMediator>();
    }
}
