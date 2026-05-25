using R3;

public class SlingshotShooterGamePauserMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly GamePauser _gamePauser;

    public SlingshotShooterGamePauserMediator(SlingshotShooter slingshotShooter, GamePauser gamePauser)
    {
        _slingshotShooter = slingshotShooter;
        _gamePauser = gamePauser;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _gamePauser.IsPaused
            .Subscribe(_slingshotShooter.SetPause)
            .AddTo(disposables);
    }
}
