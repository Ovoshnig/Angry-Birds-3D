using R3;

public class GamePauserSceneSwitchMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly SceneSwitch _sceneSwitch;

    public GamePauserSceneSwitchMediator(GamePauser gamePauser,
        SceneSwitch sceneSwitch)
    {
        _gamePauser = gamePauser;
        _sceneSwitch = sceneSwitch;
    }

    public override void Initialize()
    {
        _sceneSwitch.IsSceneLoading
            .Subscribe(OnSceneLoadingChange)
            .AddTo(CompositeDisposable);
    }

    private void OnSceneLoadingChange(bool isLoading)
    {
        if (isLoading)
            _gamePauser.Pause();
        else
            _gamePauser.UnPause();
    }
}
