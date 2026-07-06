using Cysharp.Threading.Tasks;
using R3;
using System.Threading;
using UnityEngine;

public class SlingshotBirdPlacerLevelTrackerMediator : Mediator
{
    private readonly SlingshotBirdPlacer _slingshotBirdPlacer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly BirdQueue _birdQueue;
    private readonly CameraSwitchView _cameraSwitchView;

    public SlingshotBirdPlacerLevelTrackerMediator(SlingshotBirdPlacer slingshotBirdPlacer,
        LevelStateTracker levelStateTracker, BirdQueue birdQueue, CameraSwitchView cameraSwitchView)
    {
        _slingshotBirdPlacer = slingshotBirdPlacer;
        _levelStateTracker = levelStateTracker;
        _birdQueue = birdQueue;
        _cameraSwitchView = cameraSwitchView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.MovedToNext
            .SubscribeAwait(async (_, token) => await OnMovedToNextAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnMovedToNextAsync(CancellationToken token)
    {
        await UniTask.Yield(token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        if (_slingshotBirdPlacer.CanPlace)
            if (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
                _slingshotBirdPlacer.PlaceBirdAsync(entityView.FlyerView.Rigidbody).Forget();
    }
}
