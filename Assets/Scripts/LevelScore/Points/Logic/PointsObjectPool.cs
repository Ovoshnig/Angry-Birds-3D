using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class PointsObjectPool : IDisposable
{
    private readonly ObjectPool<PointsView> _pointsPool;
    private readonly Subject<int> _pointsAdded = new();

    public PointsObjectPool(PointsView pointsPrefab, ScoreSettings scoreSettings)
    {
        Transform pointsPoolTransform = new GameObject("PointsPool").transform;

        _pointsPool = new ObjectPool<PointsView>(
            createFunc: () => Object.Instantiate(pointsPrefab, pointsPoolTransform),
            actionOnGet: pointsView => pointsView.gameObject.SetActive(true),
            actionOnRelease: pointsView => pointsView.gameObject.SetActive(false),
            defaultCapacity: scoreSettings.PoolDefaultCapacity,
            maxSize: scoreSettings.PoolMaxSize
        );
    }

    public Observable<int> PointsAdded => _pointsAdded;

    public void Dispose() => _pointsAdded.Dispose();

    public void ShowPoints(Vector3 position, PointsSettings pointsSettings)
    {
        _pointsAdded.OnNext(pointsSettings.Points);

        PointsView pointsView = _pointsPool.Get();
        pointsView.ShowAsync(position, pointsSettings).Forget();

        pointsView.Completed
            .Take(1)
            .Subscribe(_ => _pointsPool.Release(pointsView))
            .RegisterTo(pointsView.destroyCancellationToken);
    }
}
