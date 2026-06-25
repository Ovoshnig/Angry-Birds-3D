using R3;
using System;
using VContainer.Unity;

public class BirdDestroyer : IStartable, IDisposable
{
    private readonly ObjectCollider _objectCollider;
    private readonly BirdSettings _birdSettings;
    private readonly Subject<BirdEntityView> _destructionStarted = new();
    private readonly Subject<BirdEntityView> _destroyed = new();
    private readonly CompositeDisposable _disposables = new();

    public BirdDestroyer(ObjectCollider objectCollider, BirdSettings birdSettings)
    {
        _objectCollider = objectCollider;
        _birdSettings = birdSettings;
    }

    public Observable<BirdEntityView> DestructionStarted => _destructionStarted;
    public Observable<BirdEntityView> Destroyed => _destroyed;

    public void Start()
    {
        _objectCollider.Collided
            .Where(data => data.EntityView is BirdEntityView entityView && !entityView.DestroyerView.IsDestroying)
            .Select(data => data.EntityView as BirdEntityView)
            .Do(entityView =>
            {
                entityView.DestroyerView.StartDestroying();
                _destructionStarted.OnNext(entityView);
            })
            .Delay(TimeSpan.FromSeconds(_birdSettings.DestructionDelay), UnityTimeProvider.Update)
            .Subscribe(entityView =>
            {
                if (entityView != null)
                {
                    entityView.DestroyerView.Destroy();
                    _destroyed.OnNext(entityView);
                }
            })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _destructionStarted.Dispose();
        _destroyed.Dispose();
    }

    public void DestroyImmediate(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        BirdDestroyerView destroyerView = birdEntityView.DestroyerView;
        destroyerView.StartDestroying();
        _destructionStarted.OnNext(birdEntityView);

        destroyerView.Destroy();
        _destroyed.OnNext(birdEntityView);
    }
}
