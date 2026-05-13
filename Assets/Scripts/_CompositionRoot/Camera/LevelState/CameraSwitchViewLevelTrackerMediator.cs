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

    public override void Start()
    {
        _levelStateTracker.Cleared
            .Subscribe(_ => _cameraSwitchView.SwitchToSlingshotAsync().Forget())
            .AddTo(Disposables);
    }
}
