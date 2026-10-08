using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class InputActionsMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<InputActionsSceneSwitchMediator>();
    }
}
