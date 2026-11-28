using Ovoshnig.UI.Basic;
using R3;

namespace AngryBirds3D.LevelScore.Score;

public class ScoreModelViewMediator(ScoreModel scoreModel, ScoreView view) : UIViewMediator<ScoreView>(view)
{
    private readonly ScoreModel _scoreModel = scoreModel;

    protected override void OnViewEnabled(ScoreView view, CompositeDisposable viewDisposables)
    {
        _scoreModel.Score
            .Subscribe(view.SetScoreSmoothly)
            .AddTo(viewDisposables);
    }
}
