using R3;
using System.Collections.Generic;

public class RatingShowerViewsMediator : UIListMediator<RatingShowerView>
{
    private readonly RatingShower _ratingShower;

    public RatingShowerViewsMediator(RatingShower ratingShower, IReadOnlyList<RatingShowerView> views)
        : base(views) => _ratingShower = ratingShower;

    protected override void OnViewEnabled(RatingShowerView view, CompositeDisposable viewDisposables)
    {
        int starRecord = _ratingShower.GetStarRecord(view.LevelIndex);
        view.SetStarCount(starRecord);
    }
}
