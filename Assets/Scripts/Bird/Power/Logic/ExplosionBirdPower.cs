using UnityEngine;

public class ExplosionBirdPower : IBirdPower
{
    private readonly BirdExploder _birdExploder;
    private readonly ExplosionPowerSettings _powerSettings;
    private readonly Collider[] _colliders;

    public ExplosionBirdPower(BirdExploder birdExploder, ExplosionPowerSettings powerSettings)
    {
        _birdExploder = birdExploder;
        _powerSettings = powerSettings;

        _colliders = new Collider[powerSettings.MaxExplosiveCount];
    }

    public BirdPowerType Type => BirdPowerType.Explosion;

    public void Activate(BirdEntityView birdEntityView) => _birdExploder.Explode(birdEntityView.gameObject,
        _colliders, _powerSettings.ExplosionForce, _powerSettings.ExplosionRadius,
        _powerSettings.UpwardsModifier, birdEntityView.SfxProfile.ExplosionResource);
}
