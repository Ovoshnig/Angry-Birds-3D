using R3;
using System;
using VContainer.Unity;

public class ObjectDestroyer : IStartable, IDisposable
{
    private readonly ObjectCollider _objectCollider;
    private readonly Subject<DamageData> _damaged = new();
    private readonly Subject<DestructionData> _destroyed = new();
    private readonly CompositeDisposable _disposables = new();

    public ObjectDestroyer(ObjectCollider objectCollider) =>
        _objectCollider = objectCollider;

    public Observable<DamageData> Damaged => _damaged;
    public Observable<DestructionData> Destroyed => _destroyed;

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
            _destroyed.OnNext(new DestructionData(entityView));
        }
        else
        {
            destroyerView.VisualizeDamage(healthModel.Health, entityView.DestructionProfile.MaxHealth);
            _damaged.OnNext(new DamageData(entityView, data.Type, damageAmount));
        }
    }
}
