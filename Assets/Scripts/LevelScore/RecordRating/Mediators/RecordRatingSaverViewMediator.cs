using Cysharp.Threading.Tasks;
using R3;

public class RecordRatingSaverViewMediator : UIViewMediator<RecordRatingView>
{
    private readonly RecordRatingSaver _recordRatingSaver;

    public RecordRatingSaverViewMediator(RecordRatingSaver recordRatingSaver, RecordRatingView view)
        : base(view) => _recordRatingSaver = recordRatingSaver;

    protected override void OnViewEnabled(RecordRatingView view, CompositeDisposable viewDisposables)
    {
        _recordRatingSaver.Record
            .Subscribe(record => view.SetStarCount(record))
            .AddTo(viewDisposables);
    }
}
