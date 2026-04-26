using R3;
using System;
using VContainer.Unity;

public class BirdDestroyer : IInitializable, IDisposable
{
    private readonly BirdCollider _birdCollider;
    private readonly BirdSettings _birdSettings;
    private readonly Subject<BirdEntityView> _destructionStarted = new();
    private readonly Subject<BirdEntityView> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BirdDestroyer(BirdCollider birdCollider, BirdSettings birdSettings)
    {
        _birdCollider = birdCollider;
        _birdSettings = birdSettings;
    }

    public Observable<BirdEntityView> DestructionStarted => _destructionStarted;
    public Observable<BirdEntityView> Destroyed => _destroyed;

    public void Initialize()
    {
        _birdCollider.Collided
            .Where(@event => !@event.View.DestroyerView.IsDestroying)
            .Do(@event =>
            {
                @event.View.DestroyerView.StartDestroying();
                _destructionStarted.OnNext(@event.View);
            })
            .Delay(TimeSpan.FromSeconds(_birdSettings.DestructionDelay), UnityTimeProvider.Update)
            .Subscribe(@event =>
            {
                if (@event.View != null)
                {
                    @event.View.DestroyerView.Destroy();
                    _destroyed.OnNext(@event.View);
                }
            })
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
