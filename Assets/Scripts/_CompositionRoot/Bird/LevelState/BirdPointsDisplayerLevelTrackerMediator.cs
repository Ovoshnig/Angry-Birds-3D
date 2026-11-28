using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.Points;
using AngryBirds3D.Bird.Queue;
using AngryBirds3D.Camera.Switching;
using AngryBirds3D.LevelState.Tracking;
using AngryBirds3D.Slingshot.Placement;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using R3;
using System.Collections.Generic;
using System.Threading;

namespace AngryBirds3D.Composition;

public class BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
    LevelStateTracker levelStateTracker, BirdQueue birdQueue, CameraSwitchView cameraSwitchView,
    SlingshotBirdPlacer slingshotBirdPlacer) : Mediator
{
    private readonly BirdPointsDisplayer _birdPointsDisplayer = birdPointsDisplayer;
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;
    private readonly BirdQueue _birdQueue = birdQueue;
    private readonly CameraSwitchView _cameraSwitchView = cameraSwitchView;
    private readonly SlingshotBirdPlacer _slingshotBirdPlacer = slingshotBirdPlacer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Cleared
            .SubscribeAwait(async (_, token) => await OnLevelClearedAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnLevelClearedAsync(CancellationToken token)
    {
        BirdEntityView slingshotEntityView = null;

        if (_slingshotBirdPlacer.CanPlace)
        {
            if (_birdQueue.TryDequeueBird(out slingshotEntityView))
                _slingshotBirdPlacer.PlaceBirdAsync(slingshotEntityView.FlyerView.Rigidbody).Forget();
        }
        else
        {
            slingshotEntityView = _slingshotBirdPlacer.SlingshotBird.GetComponent<BirdEntityView>();
        }

        await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        List<BirdEntityView> entityViews = [];

        while (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
            entityViews.Add(entityView);

        entityViews.Add(slingshotEntityView);

        _birdPointsDisplayer.DisplaySequenceAsync(entityViews).Forget();
    }
}
