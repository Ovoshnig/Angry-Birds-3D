using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class FailureViewLevelTrackerMediator : Mediator
{
    private readonly FailurePanelView _failurePanelView;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly CameraSwitchView _cameraSwitchView;

    public FailureViewLevelTrackerMediator(FailurePanelView failurePanelView,
        LevelStateTracker levelStateTracker,
        CameraSwitchView cameraSwitchView)
    {
        _failurePanelView = failurePanelView;
        _levelStateTracker = levelStateTracker;
        _cameraSwitchView = cameraSwitchView;
    }

    public override void Start()
    {
        _levelStateTracker.Failed
            .SubscribeAwait(async (_, token) => await WaitCameraAndEnableFailurePanelAsync(token), AwaitOperation.Drop)
            .AddTo(Disposables);
    }

    private async UniTask WaitCameraAndEnableFailurePanelAsync(CancellationToken token)
    {
        await UniTask.WaitForSeconds(1, cancellationToken: token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await _cameraSwitchView.IsBlending.FirstAsync(isBlending => !isBlending, cancellationToken: token);

        _failurePanelView.SetActive(true);
    }
}
