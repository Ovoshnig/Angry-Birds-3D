using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;

namespace AngryBirds3D.LevelScore.RatingShowing;

public class RatingShowerViewsMediator(RatingShower ratingShower, IReadOnlyList<RatingShowerView> views)
    : UIViewsMediator<RatingShowerView>(views)
{
    private readonly RatingShower _ratingShower = ratingShower;

    protected override void OnViewEnabled(RatingShowerView view, CompositeDisposable viewDisposables)
    {
        int starRecord = _ratingShower.GetStarRecord(view.LevelIndex);
        view.SetStarCount(starRecord);
    }
}
