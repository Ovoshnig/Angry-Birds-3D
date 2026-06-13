using R3;

public class InputActionsSceneSwitchMediator : Mediator
{
    private readonly InputActions _inputActions;
    private readonly SceneSwitch _sceneSwitch;

    public InputActionsSceneSwitchMediator(InputActions inputActions, SceneSwitch sceneSwitch)
    {
        _inputActions = inputActions;
        _sceneSwitch = sceneSwitch;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _sceneSwitch.IsSceneLoading
            .Subscribe(isLoading =>
            {
                if (isLoading)
                    _inputActions.Disable();
                else
                    _inputActions.Enable();
            })
            .AddTo(disposables);
    }
}
