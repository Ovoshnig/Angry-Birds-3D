using R3;
using static SlingshotShooter;

public class CameraSwitchSlingshotShooterMediator : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly SlingshotShooter _slingshotShooter;

    public CameraSwitchSlingshotShooterMediator(CameraSwitchView cameraSwitchView,
        SlingshotShooter slingshotShooter)
    {
        _cameraSwitchView = cameraSwitchView;
        _slingshotShooter = slingshotShooter;
    }

    public override void Start()
    {
        _slingshotShooter.CurrentState
            .Subscribe(SlingshotStateChanged)
            .AddTo(Disposables);
    }

    private void SlingshotStateChanged(SlingshotState state)
    {
        if (state == SlingshotState.Idle)
            _cameraSwitchView.SetPriorityStructure();
        else if (state == SlingshotState.InputWaiting)
            _cameraSwitchView.SetPrioritySlingshot();
        else if (state == SlingshotState.Dragging)
            _cameraSwitchView.SetPriorityGeneral();
    }
}
