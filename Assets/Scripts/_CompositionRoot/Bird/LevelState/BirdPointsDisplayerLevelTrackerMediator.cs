using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;
using System.Threading;

public class BirdPointsDisplayerLevelTrackerMediator : Mediator
{
    private readonly BirdPointsDisplayer _birdPointsDisplayer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly BirdQueue _birdQueue;
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly SlingshotBirdPlacer _slingshotBirdPlacer;
    private readonly SlingshotShooter _slingshotShooter;

    public BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
        LevelStateTracker levelStateTracker, BirdQueue birdQueue, CameraSwitchView cameraSwitchView,
        SlingshotBirdPlacer slingshotBirdPlacer, SlingshotShooter slingshotShooter)
    {
        _birdPointsDisplayer = birdPointsDisplayer;
        _levelStateTracker = levelStateTracker;
        _birdQueue = birdQueue;
        _cameraSwitchView = cameraSwitchView;
        _slingshotBirdPlacer = slingshotBirdPlacer;
        _slingshotShooter = slingshotShooter;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Cleared
            .SubscribeAwait(async (_, token) => await OnLevelClearedAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnLevelClearedAsync(CancellationToken token)
    {
        BirdEntityView slingshotEntityView = null;

        if (_slingshotShooter.ContainsBird)
        {
            slingshotEntityView = _slingshotShooter.CurrentBird.GetComponent<BirdEntityView>();
        }
        else
        {
            if (_birdQueue.TryDequeueBird(out slingshotEntityView))
                _slingshotBirdPlacer.PlaceBirdAsync(slingshotEntityView.FlyerView.Rigidbody).Forget();
        }

        await UniTask.Yield(token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        List<BirdEntityView> entityViews = new();

        while (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
            entityViews.Add(entityView);

        entityViews.Add(slingshotEntityView);

        _birdPointsDisplayer.DisplaySequenceAsync(entityViews).Forget();
    }
}
