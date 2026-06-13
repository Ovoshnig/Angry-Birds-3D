using R3;

public class SceneSwitchLoadingScreenViewMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly LoadingScreenView _loadingScreenView;

    public SceneSwitchLoadingScreenViewMediator(SceneSwitch sceneSwitch,
        LoadingScreenView loadingScreenView)
    {
        _sceneSwitch = sceneSwitch;
        _loadingScreenView = loadingScreenView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _sceneSwitch.IsSceneLoading
            .Subscribe(isLoading =>
            {
                if (isLoading)
                    _loadingScreenView.Show();
                else
                    _loadingScreenView.Hide();
            })
            .AddTo(disposables);

        _sceneSwitch.LoadingProgress
            .Subscribe(_loadingScreenView.SetProgress)
            .AddTo(disposables);
    }
}
