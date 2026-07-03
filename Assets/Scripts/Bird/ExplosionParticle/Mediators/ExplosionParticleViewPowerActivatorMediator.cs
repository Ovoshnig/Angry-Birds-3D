using R3;
using UnityEngine;

public class ExplosionParticleViewPowerActivatorMediator : Mediator
{
    private readonly ExplosionParticleView _explosionParticleView;
    private readonly BirdPowerActivator _powerActivator;
    private readonly ExplosionPowerSettings _explosionPowerSettings;

    public ExplosionParticleViewPowerActivatorMediator(ExplosionParticleView explosionParticleView,
        BirdPowerActivator powerActivator,
        ExplosionPowerSettings explosionPowerSettings)
    {
        _explosionParticleView = explosionParticleView;
        _powerActivator = powerActivator;
        _explosionPowerSettings = explosionPowerSettings;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _powerActivator.Activated
            .Subscribe(OnActivated)
            .AddTo(disposables);
    }

    private void OnActivated(BirdEntityView entityView)
    {
        if (entityView.PowerView.PowerType != BirdPowerType.Explosion)
            return;

        Vector3 birdPosition = entityView.transform.position;
        _explosionParticleView.Play(birdPosition, _explosionPowerSettings.ExplosionRadius);
    }
}
