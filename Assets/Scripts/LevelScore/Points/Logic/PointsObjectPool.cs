using R3;
using UnityEngine;
using UnityEngine.Pool;

public class PointsObjectPool
{
    private readonly ObjectPool<PointsView> _pointsPool;

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

    public void ShowPoints(Vector3 position, PointsSettings pointsSettings)
    {
        PointsView pointsView = _pointsPool.Get();
        pointsView.Show(position, pointsSettings);

        pointsView.Completed
            .Take(1)
            .Subscribe(_ => _pointsPool.Release(pointsView))
            .RegisterTo(pointsView.destroyCancellationToken);
    }
}
