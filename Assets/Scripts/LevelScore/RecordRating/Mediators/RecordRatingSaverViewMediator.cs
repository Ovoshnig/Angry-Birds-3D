using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;

namespace AngryBirds3D.LevelScore.RecordRating;

public class RecordRatingSaverViewMediator(RecordRatingSaver recordRatingSaver, RecordRatingView view)
    : UIViewMediator<RecordRatingView>(view)
{
    private readonly RecordRatingSaver _recordRatingSaver = recordRatingSaver;

    protected override void OnViewEnabled(RecordRatingView view, CompositeDisposable viewDisposables)
    {
        _recordRatingSaver.Record
            .Subscribe(view.SetStarCount)
            .AddTo(viewDisposables);
    }
}
