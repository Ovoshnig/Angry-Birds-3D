using AngryBirds3D.Bird.Points;
using AngryBirds3D.LevelScore.RatingEvaluation;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class RatingEvaluatorBirdDisplayerMediator(RatingEvaluator ratingEvaluator,
    BirdPointsDisplayer birdPointsDisplayer) : Mediator
{
    private readonly RatingEvaluator _ratingEvaluator = ratingEvaluator;
    private readonly BirdPointsDisplayer _birdPointsDisplayer = birdPointsDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPointsDisplayer.SequenceDisplayCompleted
            .Subscribe(_ => _ratingEvaluator.Evaluate())
            .AddTo(disposables);
    }
}
