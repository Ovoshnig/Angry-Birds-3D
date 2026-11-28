using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;

namespace AngryBirds3D.LevelScore.RatingEvaluation;

public class RatingEvaluatorViewMediator(RatingEvaluator evaluator, RatingEvaluatorView view)
    : UIViewMediator<RatingEvaluatorView>(view)
{
    private readonly RatingEvaluator _evaluator = evaluator;

    protected override void OnViewEnabled(RatingEvaluatorView view, CompositeDisposable viewDisposables)
    {
        _evaluator.Rating
            .Where(count => count > 0)
            .Subscribe(rating => view.ShowStarAsync(rating).Forget())
            .AddTo(viewDisposables);
    }
}
