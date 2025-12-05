using R3;
using UnityEngine;

public record CollisionEvent<TView>(TView View, Collision Collision)
    where TView : MonoBehaviour;

public class CollisionReporter<TView> where TView : MonoBehaviour
{
    private readonly Subject<CollisionEvent<TView>> _collided = new();

    public Observable<CollisionEvent<TView>> Collided => _collided;

    public void Report(TView view, Collision collision) =>
        _collided.OnNext(new CollisionEvent<TView>(view, collision));
}
