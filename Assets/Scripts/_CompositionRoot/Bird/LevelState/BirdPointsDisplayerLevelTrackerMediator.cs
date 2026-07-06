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

    public BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
        LevelStateTracker levelStateTracker, BirdQueue birdQueue, CameraSwitchView cameraSwitchView,
        SlingshotBirdPlacer slingshotBirdPlacer)
    {
        _birdPointsDisplayer = birdPointsDisplayer;
        _levelStateTracker = levelStateTracker;
        _birdQueue = birdQueue;
        _cameraSwitchView = cameraSwitchView;
        _slingshotBirdPlacer = slingshotBirdPlacer;
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

        if (_slingshotBirdPlacer.CanPlace)
        {
            if (_birdQueue.TryDequeueBird(out slingshotEntityView))
                _slingshotBirdPlacer.PlaceBirdAsync(slingshotEntityView.FlyerView.Rigidbody).Forget();
        }
        else
        {
            slingshotEntityView = _slingshotBirdPlacer.SlingshotBird.GetComponent<BirdEntityView>();
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
