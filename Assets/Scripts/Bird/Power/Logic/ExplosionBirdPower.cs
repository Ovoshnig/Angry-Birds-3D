using UnityEngine;

public class ExplosionBirdPower : IBirdPower
{
    private readonly ExplosionPowerSettings _powerSettings;
    private readonly Collider[] _colliders;

    public ExplosionBirdPower(ExplosionPowerSettings powerSettings)
    {
        _powerSettings = powerSettings;
        _colliders = new Collider[powerSettings.MaxExplosiveCount];
    }

    public BirdPowerType Type => BirdPowerType.Explosion;

    public void Activate(BirdEntityView birdEntityView)
    {
        Vector3 birdPosition = birdEntityView.transform.position;
        Physics.OverlapSphereNonAlloc(birdPosition, _powerSettings.ExplosionRadius, _colliders);

        foreach (Collider collider in _colliders)
        {
            if (collider == null || collider.gameObject == birdEntityView.gameObject)
                continue;

            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(
                    _powerSettings.ExplosionForce,
                    birdPosition,
                    _powerSettings.ExplosionRadius,
                    _powerSettings.UpwardsModifier);
            }
        }
    }
}
