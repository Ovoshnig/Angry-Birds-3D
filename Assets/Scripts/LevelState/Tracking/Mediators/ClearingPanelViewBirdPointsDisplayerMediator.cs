using AngryBirds3D.Bird.Points;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.LevelState.Tracking;

public class ClearingPanelViewBirdPointsDisplayerMediator(ClearingPanelView clearingPanelView,
    BirdPointsDisplayer birdPointsDisplayer) : Mediator
{
    private readonly ClearingPanelView _clearingPanelView = clearingPanelView;
    private readonly BirdPointsDisplayer _birdPointsDisplayer = birdPointsDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _clearingPanelView.Hide();

        _birdPointsDisplayer.SequenceDisplayCompleted
            .Subscribe(_ => _clearingPanelView.Show())
            .AddTo(disposables);
    }
}
