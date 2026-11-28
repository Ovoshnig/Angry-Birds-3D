using AngryBirds3D.LevelScore.Score;
using Ovoshnig.UI.Basic;
using R3;

namespace AngryBirds3D.LevelState.Tracking;

public class ScoreModelFinalScoreViewMediator(ScoreModel scoreModel, FinalScoreView view) :
    UIViewMediator<FinalScoreView>(view)
{
    private readonly ScoreModel _scoreModel = scoreModel;

    protected override void OnViewEnabled(FinalScoreView view, CompositeDisposable viewDisposables) =>
        view.SetScore(_scoreModel.Score.CurrentValue);
}
