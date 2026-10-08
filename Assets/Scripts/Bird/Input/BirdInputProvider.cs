using Ovoshnig.InputProvision.Provision;
using R3;

namespace AngryBirds3D.Bird.Input
{
    public class BirdInputProvider : InputProvider<InputActions.BirdActions>
    {
        public BirdInputProvider(InputActions inputActions) : base(inputActions.Bird) =>
            UsePowerPressed = ObserveButton(a => a.UsePower);

        public ReadOnlyReactiveProperty<bool> UsePowerPressed { get; }

        protected override void EnableActions() => Actions.Enable();

        protected override void DisableActions() => Actions.Disable();
    }
}
