using VContainer;
using VContainer.Unity;

public class InputActionsMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<InputActionsSceneSwitchMediator>();
}
