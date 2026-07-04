using R3;
using UnityEngine;

public class BirdExploder
{
    private readonly Subject<BirdExplosionData> _exploded = new();

    public Observable<BirdExplosionData> Exploded => _exploded;

    public void Explode(GameObject explodingObject, Collider[] colliders, float force, float radius,
        float upwardsModifier)
    {
        Vector3 explodingObjectPosition = explodingObject.transform.position;
        Physics.OverlapSphereNonAlloc(explodingObjectPosition, radius, colliders);

        foreach (Collider collider in colliders)
        {
            if (collider == null || collider.gameObject == explodingObject)
                continue;

            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(force, explodingObjectPosition, radius, upwardsModifier);
        }

        _exploded.OnNext(new BirdExplosionData(explodingObjectPosition, force, radius));
    }
}
