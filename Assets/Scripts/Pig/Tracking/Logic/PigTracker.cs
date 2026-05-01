using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

public class PigTracker : IStartable, IDisposable
{
    private readonly List<PigEntityView> _pigEntityViews;
    private readonly PigDestroyer _pigDestroyer;
    private readonly ReactiveProperty<int> _pigCount = new();
    private readonly CompositeDisposable _disposables = new();

    public PigTracker(PigEntityView[] pigEntityViews, PigDestroyer pigDestroyer)
    {
        _pigEntityViews = pigEntityViews.ToList();
        _pigDestroyer = pigDestroyer;

        _pigCount.Value = pigEntityViews.Length;

        PigsLeft = _pigCount
            .Where(count => count == 0)
            .Select(_ => Unit.Default)
            .Share();
    }

    public ReadOnlyReactiveProperty<int> PigCount => _pigCount;
    public Observable<Unit> PigsLeft { get; }

    public void Start()
    {
        _pigDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _pigCount.Dispose();
    }

    private void OnDestroyed(DestructionEvent<PigEntityView> @event)
    {
        if (_pigEntityViews.Contains(@event.EntityView))
        {
            _pigEntityViews.Remove(@event.EntityView);
            _pigCount.Value--;
        }
        else
        {
            Debug.LogWarning("Destroyed pig not contains in the list");
        }
    }
}
