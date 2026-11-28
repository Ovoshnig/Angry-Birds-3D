using AngryBirds3D.Slingshot.Shooting;
using Ovoshnig.GameState.Pause;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SlingshotShooterGamePauserMediator(SlingshotShooter slingshotShooter, GamePauser gamePauser)
    : Mediator
{
    private readonly SlingshotShooter _slingshotShooter = slingshotShooter;
    private readonly GamePauser _gamePauser = gamePauser;

    protected override void Bind(CompositeDisposable disposables)
    {
        _gamePauser.IsPaused
            .Subscribe(_slingshotShooter.SetPause)
            .AddTo(disposables);
    }
}
