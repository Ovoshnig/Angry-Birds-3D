using R3;

public class RatingEvaluatorBirdDisplayerMediator : Mediator
{
    private readonly RatingEvaluator _ratingEvaluator;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;

    public RatingEvaluatorBirdDisplayerMediator(RatingEvaluator ratingEvaluator,
        BirdPointsDisplayer birdPointsDisplayer)
    {
        _ratingEvaluator = ratingEvaluator;
        _birdPointsDisplayer = birdPointsDisplayer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPointsDisplayer.SequenceDisplayCompleted
            .Subscribe(_ => _ratingEvaluator.Evaluate())
            .AddTo(disposables);
    }
}
