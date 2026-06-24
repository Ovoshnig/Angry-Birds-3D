using R3;
using System;
using VContainer.Unity;

public class BirdPowerActivator : IStartable, IDisposable
{
    private readonly BirdFlyer _birdFlyer;
    private readonly BirdInputProvider _inputProvider;
    private readonly BirdPowerRegistry _powerRegistry;
    private readonly BirdPowerSettings _powerSettings;
    private readonly Subject<BirdEntityView> _activated = new();
    private readonly CompositeDisposable _flightDisposables = new();
    private readonly CompositeDisposable _inputDisposables = new();

    public BirdPowerActivator(BirdFlyer birdFlyer,
        BirdInputProvider inputProvider,
        BirdPowerRegistry powerRegistry,
        BirdPowerSettings powerSettings)
    {
        _birdFlyer = birdFlyer;
        _inputProvider = inputProvider;
        _powerRegistry = powerRegistry;
        _powerSettings = powerSettings;
    }

    public Observable<BirdEntityView> Activated => _activated;

    public void Start()
    {
        _birdFlyer.FlightStarted
            .Subscribe(OnFlightStarted)
            .AddTo(_flightDisposables);

        _birdFlyer.FlightInterrupted
            .Subscribe(OnFlightInterrupted)
            .AddTo(_flightDisposables);
    }

    public void Dispose()
    {
        _flightDisposables.Dispose();
        _inputDisposables.Dispose();
    }

    private void OnFlightStarted(BirdEntityView birdEntityView)
    {
        _inputProvider.UsePowerPressed
            .Pairwise()
            .Where(isPressed => !isPressed.Previous && isPressed.Current)
            .Take(1)
            .Subscribe(_ => OnUsePowerPressed(birdEntityView))
            .AddTo(_inputDisposables);
    }

    private void OnFlightInterrupted(BirdEntityView _) => _inputDisposables.Clear();

    private void OnUsePowerPressed(BirdEntityView birdEntityView)
    {
        BirdPowerType powerType = birdEntityView.PowerView.PowerType;

        if (powerType == BirdPowerType.None)
            return;

        if (_powerRegistry.TryGet(powerType, out IBirdPower power))
        {
            power.Activate(birdEntityView, _powerSettings);
            _activated.OnNext(birdEntityView);
        }
    }
}
