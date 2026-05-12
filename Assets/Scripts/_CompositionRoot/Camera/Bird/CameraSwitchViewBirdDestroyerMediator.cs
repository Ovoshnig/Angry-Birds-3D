using Cysharp.Threading.Tasks;
using R3;

public class CameraSwitchViewBirdDestroyerMediator : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly PigTracker _pigTracker;

    public CameraSwitchViewBirdDestroyerMediator(CameraSwitchView cameraSwitchView,
        BirdDestroyer birdDestroyer,
        PigTracker pigTracker)
    {
        _cameraSwitchView = cameraSwitchView;
        _birdDestroyer = birdDestroyer;
        _pigTracker = pigTracker;
    }

    public override void Start()
    {
        _birdDestroyer.Destroyed
            .Subscribe(_ =>
            {
                if (_pigTracker.PigCount.CurrentValue > 0)
                    _cameraSwitchView.SwitchToGeneralAsync().Forget();
            })
            .AddTo(Disposables);
    }
}
