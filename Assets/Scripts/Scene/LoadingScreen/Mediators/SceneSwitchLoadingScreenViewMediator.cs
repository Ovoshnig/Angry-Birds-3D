using Ovoshnig.Scene.Switching;
using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Scene.LoadingScreen
{
    public class SceneSwitchLoadingScreenViewMediator : UIViewMediator<LoadingScreenView>
    {
        private readonly SceneSwitch _sceneSwitch;
        private readonly LoadingScreenView _loadingScreenView;

        public SceneSwitchLoadingScreenViewMediator(SceneSwitch sceneSwitch, LoadingScreenView view)
            : base(view)
        {
            _sceneSwitch = sceneSwitch;
            _loadingScreenView = view;
        }

        protected override void Bind(CompositeDisposable disposables)
        {
            base.Bind(disposables);

            _sceneSwitch.IsSceneLoading
                .Subscribe(isLoading =>
                {
                    if (isLoading)
                        _loadingScreenView.Show();
                    else
                        _loadingScreenView.Hide();
                })
                .AddTo(disposables);
        }

        protected override void OnViewEnabled(LoadingScreenView view, CompositeDisposable viewDisposables)
        {
            _sceneSwitch.LoadingProgress
                .Subscribe(view.SetProgress)
                .AddTo(viewDisposables);
        }
    }
}
