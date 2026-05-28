using Cysharp.Threading.Tasks;
using R3;

public class RecordRatingViewMediator : UIMediator<RecordRatingView>
{
    private readonly RecordRatingSaver _recordRatingSaver;

    public RecordRatingViewMediator(RecordRatingSaver recordRatingSaver, RecordRatingView view)
        : base(view) => _recordRatingSaver = recordRatingSaver;

    protected override void OnViewEnabled(RecordRatingView view, CompositeDisposable viewDisposables)
    {
        _recordRatingSaver.Record
            .Subscribe(record => view.SetStarCount(record))
            .AddTo(viewDisposables);
    }
}
