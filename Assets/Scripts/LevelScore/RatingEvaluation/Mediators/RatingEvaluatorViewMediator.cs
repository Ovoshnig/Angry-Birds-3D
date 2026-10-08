using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;

namespace AngryBirds3D.LevelScore.RatingEvaluation
{
    public class RatingEvaluatorViewMediator : UIViewMediator<RatingEvaluatorView>
    {
        private readonly RatingEvaluator _evaluator;

        public RatingEvaluatorViewMediator(RatingEvaluator evaluator, RatingEvaluatorView view)
            : base(view) => _evaluator = evaluator;

        protected override void OnViewEnabled(RatingEvaluatorView view, CompositeDisposable viewDisposables)
        {
            _evaluator.Rating
                .Subscribe(rating => view.ShowStarAsync(rating).Forget())
                .AddTo(viewDisposables);
        }
    }
}
