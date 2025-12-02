using R3;
using UnityEngine;

public class SlingshotInputHandler : InputHandler<InputActions.SlingshotActions>
{
    public SlingshotInputHandler(InputActions inputActions) : base(inputActions.Slingshot)
    { }

    public ReadOnlyReactiveProperty<bool> LeftButtonPressed { get; private set; }
    public ReadOnlyReactiveProperty<Vector2> DragInput { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        LeftButtonPressed = BindButton(a => a.LeftButton);
        DragInput = BindValue<Vector2>(a => a.Drag);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
