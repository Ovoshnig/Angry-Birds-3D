using R3;
using UnityEngine;

public record CollisionEvent<TView>(TView View, Collision Collision)
    where TView : MonoBehaviour;

public class CollisionReporter<TView> where TView : MonoBehaviour
{
    private readonly Subject<CollisionEvent<TView>> _collided = new();

    public Observable<CollisionEvent<TView>> Collided => _collided;

    public void Report(TView colliderView, Collision collision) =>
        _collided.OnNext(new CollisionEvent<TView>(colliderView, collision));
}
