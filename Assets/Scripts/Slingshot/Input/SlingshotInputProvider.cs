using R3;
using UnityEngine;

public class SlingshotInputProvider : InputProvider<InputActions.SlingshotActions>
{
    public SlingshotInputProvider(InputActions inputActions) : base(inputActions.Slingshot)
    {
        LeftButtonPressed = ObserveButton(a => a.LeftButton);
        DragInput = ObserveValue<Vector2>(a => a.Drag);
    }

    public ReadOnlyReactiveProperty<bool> LeftButtonPressed { get; }
    public ReadOnlyReactiveProperty<Vector2> DragInput { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
