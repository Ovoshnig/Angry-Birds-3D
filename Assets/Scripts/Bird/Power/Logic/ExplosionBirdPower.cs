using UnityEngine;

public class ExplosionBirdPower : IBirdPower
{
    public BirdPowerType Type => BirdPowerType.Explosion;

    public void Activate(BirdEntityView birdEntityView, BirdPowerSettings powerSettings)
    {
        Vector3 birdPosition = birdEntityView.transform.position;
        Collider[] colliders = new Collider[powerSettings.MaxExplosiveCount];
        Physics.OverlapSphereNonAlloc(birdPosition, powerSettings.ExplosionRadius, colliders);

        foreach (Collider collider in colliders)
        {
            if (collider == null || collider.gameObject == birdEntityView.gameObject)
                continue;

            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(
                    powerSettings.ExplosionForce,
                    birdPosition,
                    powerSettings.ExplosionRadius,
                    powerSettings.UpwardsModifier);
            }
        }
    }
}
