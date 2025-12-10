using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class PointsObjectPool : IDisposable
{
    private readonly ObjectPool<PointsView> _pointsPool;
    private readonly Dictionary<PointsView, IDisposable> _subscriptions = new();

    public PointsObjectPool(PointsView pointsPrefab, ScoreSettings scoreSettings)
    {
        _pointsPool = new ObjectPool<PointsView>(
            createFunc: () => Object.Instantiate(pointsPrefab),
            actionOnGet: pointsView => pointsView.gameObject.SetActive(true),
            actionOnRelease: OnRelease,
            defaultCapacity: scoreSettings.PoolDefaultCapacity,
            maxSize: scoreSettings.PoolMaxSize
            );
    }

    public void Dispose()
    {
        foreach (var subscription in _subscriptions.Values)
            subscription.Dispose();

        _subscriptions.Clear();
    }

    public void ShowPoints(Vector3 position, int points)
    {
        PointsView pointsView = _pointsPool.Get();
        pointsView.Show(position, points);

        IDisposable subscription = pointsView.IsPlaying
            .Where(isPlaying => !isPlaying)
            .Subscribe(_ => _pointsPool.Release(pointsView));

        _subscriptions[pointsView] = subscription;
    }

    private void OnRelease(PointsView pointsView)
    {
        _subscriptions[pointsView].Dispose();
        _subscriptions.Remove(pointsView);
        pointsView.gameObject.SetActive(false);
    }
}
