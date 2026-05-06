using R3;
using UnityEngine;
using VContainer.Unity;

public class SlingshotInputHandler : InputProvider<InputActions.SlingshotActions>, IInitializable
{
    public SlingshotInputHandler(InputActions inputActions) : base(inputActions.Slingshot)
    { }

    public ReadOnlyReactiveProperty<bool> LeftButtonPressed { get; private set; }
    public ReadOnlyReactiveProperty<Vector2> DragInput { get; private set; }

    public void Initialize()
    {
        LeftButtonPressed = ObserveButton(a => a.LeftButton);
        DragInput = ObserveValue<Vector2>(a => a.Drag);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
