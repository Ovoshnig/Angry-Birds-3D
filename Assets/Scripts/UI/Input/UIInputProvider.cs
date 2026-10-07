using Ovoshnig.InputProvision.Provision;
using R3;

namespace Ovoshnig.UI.Input
{
    public class UIInputProvider : InputProvider<InputActions.UIActions>
    {
        public UIInputProvider(InputActions inputActions) : base(inputActions.UI)
        {
            CancelPressed = ObserveButton(a => a.Cancel);
            ClickPressed = ObserveButton(a => a.Click);
            SkipTextPrintingPressed = ObserveButton(a => a.SkipTextPrinting);
        }

        public ReadOnlyReactiveProperty<bool> CancelPressed { get; }
        public ReadOnlyReactiveProperty<bool> ClickPressed { get; }
        public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; }

        protected override void EnableActions() => Actions.Enable();

        protected override void DisableActions() => Actions.Disable();
    }
}
