using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.GameState.Quitting
{
    public class GameQuitterButtonViewMediator : UIViewMediator<GameQuitButtonView>
    {
        private readonly GameQuitter _gameQuitter;

        public GameQuitterButtonViewMediator(GameQuitter gameQuitter, GameQuitButtonView view)
            : base(view) => _gameQuitter = gameQuitter;

        protected override void OnViewEnabled(GameQuitButtonView view, CompositeDisposable viewDisposables)
        {
            view.Clicked
                .Subscribe(_ => _gameQuitter.Quit())
                .AddTo(viewDisposables);
        }
    }
}
