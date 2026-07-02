using Cysharp.Threading.Tasks;
using R3;
using System.Threading;

public class BirdPowerActivatorBirdFlyerMediator : Mediator
{
    private readonly BirdPowerActivator _birdPowerActivator;
    private readonly BirdFlyer _birdFlyer;
    private readonly BirdPowerSettings _birdPowerSettings;

    public BirdPowerActivatorBirdFlyerMediator(BirdPowerActivator birdPowerActivator,
        BirdFlyer birdFlyer,
        BirdPowerSettings birdPowerSettings)
    {
        _birdPowerActivator = birdPowerActivator;
        _birdFlyer = birdFlyer;
        _birdPowerSettings = birdPowerSettings;
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
            await UniTask.WaitForSeconds(_birdPowerSettings.ExplosionDelay, cancellationToken: token);

            _birdPowerActivator.ActivatePower(entityView);
        }
    }
}
