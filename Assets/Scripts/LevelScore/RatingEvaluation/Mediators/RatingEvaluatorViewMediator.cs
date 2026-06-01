using Cysharp.Threading.Tasks;
using R3;

public class RatingEvaluatorViewMediator : UIViewMediator<RatingEvaluatorView>
{
    private readonly RatingEvaluator _evaluator;

    public RatingEvaluatorViewMediator(RatingEvaluator evaluator, RatingEvaluatorView view)
        : base(view) => _evaluator = evaluator;

    protected override void OnViewEnabled(RatingEvaluatorView view, CompositeDisposable viewDisposables)
    {
        int starCount = _evaluator.EvaluateStarCount(view.MaxScoreThreshold, view.MaxStarCount);
        view.ShowStarAsync(starCount).Forget();
    }
}
