using Ovoshnig.Mediation;
using Ovoshnig.Scene.Switching;
using R3;

namespace AngryBirds3D.Composition;

public class InputActionsSceneSwitchMediator(InputActions inputActions, SceneSwitch sceneSwitch) : Mediator
{
    private readonly InputActions _inputActions = inputActions;
    private readonly SceneSwitch _sceneSwitch = sceneSwitch;

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
