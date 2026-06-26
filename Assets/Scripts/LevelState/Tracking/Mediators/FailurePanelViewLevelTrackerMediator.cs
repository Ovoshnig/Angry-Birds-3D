using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class FailurePanelViewLevelTrackerMediator : Mediator
{
    private readonly FailurePanelView _failurePanelView;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly CameraSwitchView _cameraSwitchView;

    public FailurePanelViewLevelTrackerMediator(FailurePanelView failurePanelView,
        LevelStateTracker levelStateTracker,
        CameraSwitchView cameraSwitchView)
    {
        _failurePanelView = failurePanelView;
        _levelStateTracker = levelStateTracker;
        _cameraSwitchView = cameraSwitchView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _failurePanelView.Hide();

        _levelStateTracker.Failed
            .SubscribeAwait(async (_, token) => await OnLevelFailedAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnLevelFailedAsync(CancellationToken token)
    {
        await UniTask.WaitForSeconds(1, cancellationToken: token);

        if (_cameraSwitchView.IsBlending.CurrentValue)
            await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        _failurePanelView.Show();
    }
}
