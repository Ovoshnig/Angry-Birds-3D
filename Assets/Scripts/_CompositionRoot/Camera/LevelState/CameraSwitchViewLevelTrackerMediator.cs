using Cysharp.Threading.Tasks;
using R3;

public class CameraSwitchViewLevelTrackerMediator : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly LevelStateTracker _levelStateTracker;

    public CameraSwitchViewLevelTrackerMediator(CameraSwitchView cameraSwitchView,
        LevelStateTracker levelStateTracker)
    {
        _cameraSwitchView = cameraSwitchView;
        _levelStateTracker = levelStateTracker;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Cleared
            .Subscribe(_ => _cameraSwitchView.SwitchToSlingshotAsync().Forget())
            .AddTo(disposables);
    }
}
