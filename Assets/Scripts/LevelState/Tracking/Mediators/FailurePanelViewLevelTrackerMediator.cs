using AngryBirds3D.Camera.Switching;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using R3;
using System.Threading;

namespace AngryBirds3D.LevelState.Tracking;

public class FailurePanelViewLevelTrackerMediator(FailurePanelView failurePanelView,
    LevelStateTracker levelStateTracker, CameraSwitchView cameraSwitchView) : Mediator
{
    private readonly FailurePanelView _failurePanelView = failurePanelView;
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;
    private readonly CameraSwitchView _cameraSwitchView = cameraSwitchView;

    protected override void Bind(CompositeDisposable disposables)
    {
        _failurePanelView.Hide();

        _levelStateTracker.Failed
            .SubscribeAwait(async (_, token) => await OnLevelFailedAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnLevelFailedAsync(CancellationToken token)
    {
        await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);
        _failurePanelView.Show();
    }
}
