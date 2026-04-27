using R3;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectColliderView : MonoBehaviour
{
    private readonly Subject<Collision> _collided = new();

    public Observable<Collision> Collided => _collided;

    private void OnCollisionEnter(Collision collision) => _collided.OnNext(collision);

    private void OnDestroy() => _collided.Dispose();
}
