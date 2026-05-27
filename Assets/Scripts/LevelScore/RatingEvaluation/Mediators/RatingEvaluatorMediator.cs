using Cysharp.Threading.Tasks;
using R3;

public class RatingEvaluatorMediator : UIMediator<RatingEvaluatorView>
{
    private readonly RatingEvaluator _evaluator;

    public RatingEvaluatorMediator(RatingEvaluator evaluator, RatingEvaluatorView view)
        : base(view) => _evaluator = evaluator;

    protected override void OnViewEnabled(RatingEvaluatorView view, CompositeDisposable viewDisposables)
    {
        int starCount = _evaluator.EvaluateStarCount(view.MaxScoreThreshold, view.MaxStarCount);
        view.SetStarCount(starCount).Forget();
    }
}
