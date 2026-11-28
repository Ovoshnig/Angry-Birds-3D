using AngryBirds3D.Pig.Entity;
using Ovoshnig.ObjectDestruction.Destruction;
using Ovoshnig.ObjectDestruction.Entity;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace AngryBirds3D.Pig.Tracking;

public sealed class PigTracker : IStartable, IDisposable
{
    private readonly List<PigEntityView> _pigEntityViews;
    private readonly ObjectDestroyer _objectDestroyer;
    private readonly ReactiveProperty<int> _pigCount = new();
    private readonly CompositeDisposable _disposables = [];

    public PigTracker(IReadOnlyList<PigEntityView> pigEntityViews, ObjectDestroyer objectDestroyer)
    {
        _pigEntityViews = [.. pigEntityViews];
        _objectDestroyer = objectDestroyer;

        _pigCount.Value = pigEntityViews.Count;

        PigsLeft = _pigCount
            .Where(count => count == 0)
            .AsUnitObservable()
            .Share();
    }

    public ReadOnlyReactiveProperty<int> PigCount => _pigCount;
    public Observable<Unit> PigsLeft { get; }
    public bool AnyPigs => _pigCount.Value > 0;

    public void Start()
    {
        _objectDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _pigCount.Dispose();
    }

    private void OnDestroyed(DestructibleEntityView entityView)
    {
        if (entityView is not PigEntityView pigEntityView)
            return;

        if (_pigEntityViews.Remove(pigEntityView))
            _pigCount.Value--;
        else
            Debug.LogWarning("Destroyed pig not contains in the list");
    }
}
