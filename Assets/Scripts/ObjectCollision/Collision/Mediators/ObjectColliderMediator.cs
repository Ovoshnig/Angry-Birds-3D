using R3;
using System;
using UnityEngine;

public abstract class ObjectColliderMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly ObjectCollider<TView> _objectCollider;

    protected ObjectColliderMediator(ObjectCollider<TView> objectCollider) =>
        _objectCollider = objectCollider;

    protected void Subscribe(TView view, ObjectColliderView colliderView)
    {
        colliderView.Collided
            .Subscribe(collision => _objectCollider.Collide(view, collision))
            .AddTo(CompositeDisposable);
    }

    protected void Subscribe(TView view,
        ObjectColliderView colliderView,
        out IDisposable disposable)
    {
        disposable = colliderView.Collided
            .Subscribe(collision => _objectCollider.Collide(view, collision));
    }
}
