using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.GameState.Quitting;

public class GameQuitterButtonViewMediator(GameQuitButtonView view) : UIViewMediator<GameQuitButtonView>(view)
{
    protected override void OnViewEnabled(GameQuitButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => GameQuitter.Quit())
            .AddTo(viewDisposables);
    }
}
