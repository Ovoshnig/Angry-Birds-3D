using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class BirdPointsDisplayerLevelTrackerMediator : Mediator
{
    private readonly BirdPointsDisplayer _birdPointsDisplayer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly CameraSwitchView _cameraSwitchView;

    public BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
        LevelStateTracker levelStateTracker,
        CameraSwitchView cameraSwitchView)
    {
        _birdPointsDisplayer = birdPointsDisplayer;
        _levelStateTracker = levelStateTracker;
        _cameraSwitchView = cameraSwitchView;
    }

    public override void Start()
    {
        _levelStateTracker.Completed
            .SubscribeAwait(async (_, token) => await WaitCameraAndDisplayPointsAsync(token), AwaitOperation.Drop)
            .AddTo(Disposables);
    }

    private async UniTask WaitCameraAndDisplayPointsAsync(CancellationToken token)
    {
        await UniTask.Yield(cancellationToken: token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await _cameraSwitchView.IsBlending.FirstAsync(isBlending => !isBlending, cancellationToken: token);

        await _birdPointsDisplayer.DisplayPointsAsync();
    }
}
