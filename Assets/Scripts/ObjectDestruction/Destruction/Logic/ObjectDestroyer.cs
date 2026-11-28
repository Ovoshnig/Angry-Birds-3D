using Ovoshnig.ObjectCollision.Collision;
using Ovoshnig.ObjectDestruction.Entity;
using Ovoshnig.ObjectHealth.Health;
using R3;
using System;
using VContainer.Unity;

namespace Ovoshnig.ObjectDestruction.Destruction;

public sealed class ObjectDestroyer(ObjectCollider objectCollider) : IStartable, IDisposable
{
    private readonly ObjectCollider _objectCollider = objectCollider;
    private readonly Subject<DamageData> _damaged = new();
    private readonly Subject<DestructibleEntityView> _destroyed = new();
    private readonly CompositeDisposable _disposables = [];

    public Observable<DamageData> Damaged => _damaged;
    public Observable<DestructibleEntityView> Destroyed => _destroyed;

    public void Start()
    {
        _objectCollider.Collided
            .Subscribe(OnCollided)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _damaged.Dispose();
        _destroyed.Dispose();
    }

    private void OnCollided(CollisionData data)
    {
        if (data.EntityView is not DestructibleEntityView entityView)
            return;

        float damageAmount = data.Force;
        HealthModel healthModel = entityView.HealthModel;
        healthModel.ApplyDamage(damageAmount);

        ObjectDestroyerView destroyerView = entityView.DestroyerView;

        if (healthModel.Health <= 0)
        {
            destroyerView.Destroy();
            _destroyed.OnNext(entityView);
        }
        else
        {
            destroyerView.VisualizeDamage(data.Point, healthModel.Health, entityView.DestructionProfile.MaxHealth);
            _damaged.OnNext(new DamageData(entityView, data.Type, damageAmount));
        }
    }
}
