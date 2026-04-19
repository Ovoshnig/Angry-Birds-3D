using R3;
using UnityEngine;

public abstract class ObjectDestroyerMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly ObjectDestroyer<TView> _objectDestroyer;

    public ObjectDestroyerMediator(ObjectDestroyer<TView> objectDestroyer) =>
        _objectDestroyer = objectDestroyer;

    public override void Initialize()
    {
        _objectDestroyer.Damaged
            .Subscribe(damageEvent =>
            _objectDestroyer.GetObjectDestroyerView(damageEvent.EntityView).Damage(damageEvent.Damage))
            .AddTo(CompositeDisposable);

        _objectDestroyer.Destroyed
            .Subscribe(destructionEvent =>
            _objectDestroyer.GetObjectDestroyerView(destructionEvent.EntityView).Destroy())
            .AddTo(CompositeDisposable);
    }
}
