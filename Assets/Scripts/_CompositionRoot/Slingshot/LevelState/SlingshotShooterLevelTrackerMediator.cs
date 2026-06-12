using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class SlingshotShooterLevelTrackerMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly SlingshotBirdPlacer _slingshotBirdPlacer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly BirdQueue _birdQueue;
    private readonly CameraSwitchView _cameraSwitchView;

    public SlingshotShooterLevelTrackerMediator(SlingshotShooter slingshotShooter,
        SlingshotBirdPlacer slingshotBirdPlacer,
        LevelStateTracker levelStateTracker,
        BirdQueue birdQueue,
        CameraSwitchView cameraSwitchView)
    {
        _slingshotShooter = slingshotShooter;
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

        _levelStateTracker.Completed
            .Subscribe(_ =>
            {
                TrySetBirdInSlingshot();
                _slingshotShooter.StopShooting();
            })
            .AddTo(disposables);
    }

    private async UniTask OnMovedToNextAsync(CancellationToken token)
    {
        await UniTask.Yield(token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        TrySetBirdInSlingshot();
    }

    private bool TrySetBirdInSlingshot()
    {
        if (!_slingshotBirdPlacer.IsPlacing
            && _slingshotShooter.CurrentBird == null
            && _birdQueue.TryDequeueBird(out BirdEntityView birdEntityView))
        {
            _slingshotBirdPlacer.PlaceBirdAsync(birdEntityView.FlyerView.Rigidbody).Forget();
            return true;
        }

        return false;
    }
}
