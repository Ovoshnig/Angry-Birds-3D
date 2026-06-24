using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

public class PigTracker : IStartable, IDisposable
{
    private readonly List<PigEntityView> _pigEntityViews;
    private readonly ObjectDestroyer _objectDestroyer;
    private readonly ReactiveProperty<int> _pigCount = new();
    private readonly CompositeDisposable _disposables = new();

    public PigTracker(IReadOnlyList<PigEntityView> pigEntityViews, ObjectDestroyer objectDestroyer)
    {
        _pigEntityViews = pigEntityViews.ToList();
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

    private void OnDestroyed(DestructionData data)
    {
        if (data.EntityView is not PigEntityView pigEntityView)
            return;

        if (_pigEntityViews.Contains(pigEntityView))
        {
            _pigEntityViews.Remove(pigEntityView);
            _pigCount.Value--;
        }
        else
        {
            Debug.LogWarning("Destroyed pig not contains in the list");
        }
    }
}
