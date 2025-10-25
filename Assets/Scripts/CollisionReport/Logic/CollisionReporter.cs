using R3;
using UnityEngine;

public record CollidedEvent<TView>(TView View, Collider Other)
    where TView : MonoBehaviour;

public class CollisionReporter<TView> where TView : MonoBehaviour
{
    private readonly Subject<CollidedEvent<TView>> _collided = new();

    public Observable<CollidedEvent<TView>> Collided => _collided;

    public void Report(TView colliderView, Collider other) =>
        _collided.OnNext(new CollidedEvent<TView>(colliderView, other));
}
