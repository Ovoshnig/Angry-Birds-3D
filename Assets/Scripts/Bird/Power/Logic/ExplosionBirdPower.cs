using AngryBirds3D.Bird.Entity;
using Ovoshnig.GameSettings.BirdPower;
using UnityEngine;

namespace AngryBirds3D.Bird.Power;

public class ExplosionBirdPower(BirdExploder birdExploder, ExplosionPowerSettings powerSettings) : IBirdPower
{
    private readonly BirdExploder _birdExploder = birdExploder;
    private readonly ExplosionPowerSettings _powerSettings = powerSettings;
    private readonly Collider[] _colliders = new Collider[powerSettings.MaxExplosiveCount];

    public BirdPowerType Type => BirdPowerType.Explosion;

    public void Activate(BirdEntityView birdEntityView) => _birdExploder.Explode(birdEntityView.gameObject,
        _colliders, _powerSettings.ExplosionForce, _powerSettings.ExplosionRadius,
        _powerSettings.UpwardsModifier, birdEntityView.SfxProfile.ExplosionResource);
}
