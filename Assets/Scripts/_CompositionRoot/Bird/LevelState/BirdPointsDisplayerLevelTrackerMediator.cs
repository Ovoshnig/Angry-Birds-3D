using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class BirdPointsDisplayerLevelTrackerMediator : Mediator
{
    private readonly BirdPointsDisplayer _birdPointsDisplayer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly SlingshotShooter _slingshotShooter;
    private readonly CameraSwitchView _cameraSwitchView;

    public BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
        LevelStateTracker levelStateTracker,
        SlingshotShooter slingshotShooter,
        CameraSwitchView cameraSwitchView)
    {
        _birdPointsDisplayer = birdPointsDisplayer;
        _levelStateTracker = levelStateTracker;
        _slingshotShooter = slingshotShooter;
        _cameraSwitchView = cameraSwitchView;
    }

    public override void Start()
    {
        _levelStateTracker.Cleared
            .SubscribeAwait(async (_, token) => await OnLevelClearedAsync(token), AwaitOperation.Drop)
            .AddTo(Disposables);
    }

    private async UniTask OnLevelClearedAsync(CancellationToken token)
    {
        if (_slingshotShooter.CurrentBird != null)
        {
            BirdEntityView slingshotBird = _slingshotShooter.CurrentBird.GetComponent<BirdEntityView>();
            _birdPointsDisplayer.SetSlingshotBird(slingshotBird);
        }

        await UniTask.WaitForSeconds(1, cancellationToken: token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await _cameraSwitchView.IsBlending.FirstAsync(isBlending => !isBlending, cancellationToken: token);

        await _birdPointsDisplayer.DisplayBirdSequenceAsync();
    }
}
