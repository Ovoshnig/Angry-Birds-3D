using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class BirdPowerActivatorBirdFlyerMediator : Mediator
{
    private readonly BirdPowerActivator _birdPowerActivator;
    private readonly BirdFlyer _birdFlyer;
    private readonly ExplosionPowerSettings _explosionPowerSettings;

    public BirdPowerActivatorBirdFlyerMediator(BirdPowerActivator birdPowerActivator,
        BirdFlyer birdFlyer,
        ExplosionPowerSettings explosionPowerSettings)
    {
        _birdPowerActivator = birdPowerActivator;
        _birdFlyer = birdFlyer;
        _explosionPowerSettings = explosionPowerSettings;
    }

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
