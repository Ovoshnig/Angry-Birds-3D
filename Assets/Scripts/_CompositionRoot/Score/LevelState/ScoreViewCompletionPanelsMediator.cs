using AngryBirds3D.LevelScore.Score;
using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class ScoreViewCompletionPanelsMediator(ScoreView scoreView, ClearingPanelView clearingPanelView,
    FailurePanelView failurePanelView) : Mediator
{
    private readonly ScoreView _scoreView = scoreView;
    private readonly ClearingPanelView _clearingPanelView = clearingPanelView;
    private readonly FailurePanelView _failurePanelView = failurePanelView;

    protected override void Bind(CompositeDisposable disposables)
    {
        Observable.Merge(_clearingPanelView.Shown, _failurePanelView.Shown)
            .Subscribe(_ => _scoreView.gameObject.SetActive(false))
            .AddTo(disposables);
    }
}
