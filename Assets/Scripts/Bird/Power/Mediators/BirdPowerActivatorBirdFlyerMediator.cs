using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.Flight;
using Cysharp.Threading.Tasks;
using Ovoshnig.GameSettings.BirdPower;
using Ovoshnig.Mediation;
using R3;
using System.Threading;

namespace AngryBirds3D.Bird.Power;

public class BirdPowerActivatorBirdFlyerMediator(BirdPowerActivator birdPowerActivator, BirdFlyer birdFlyer,
    ExplosionPowerSettings explosionPowerSettings) : Mediator
{
    private readonly BirdPowerActivator _birdPowerActivator = birdPowerActivator;
    private readonly BirdFlyer _birdFlyer = birdFlyer;
    private readonly ExplosionPowerSettings _explosionPowerSettings = explosionPowerSettings;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdFlyer.FlightInterrupted
            .SubscribeAwait(async (birdEntityView, token) =>
                await OnFlightInterruptedAsync(birdEntityView, token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnFlightInterruptedAsync(BirdEntityView entityView, CancellationToken token)
    {
        BirdPowerView powerView = entityView.PowerView;

        if (powerView.PowerType == BirdPowerType.Explosion && !powerView.WasActivated)
        {
            await UniTask.WaitForSeconds(_explosionPowerSettings.ExplosionDelay, cancellationToken: token);

            _birdPowerActivator.ActivatePower(entityView);
        }
    }
}
