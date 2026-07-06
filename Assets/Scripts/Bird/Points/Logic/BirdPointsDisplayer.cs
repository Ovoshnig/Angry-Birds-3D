using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BirdPointsDisplayer : IDisposable
{
    private readonly Subject<BirdPointsDisplayData> _pointsDisplayStarted = new();
    private readonly Subject<Unit> _sequenceDisplayCompleted = new();
    private readonly CancellationTokenSource _cts = new();

    public Observable<BirdPointsDisplayData> PointsDisplayStarted => _pointsDisplayStarted;
    public Observable<Unit> SequenceDisplayCompleted => _sequenceDisplayCompleted;

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public async UniTask DisplaySequenceAsync(IReadOnlyList<BirdEntityView> entityViews)
    {
        foreach (var entityView in entityViews)
            if (entityView != null)
                await DisplayPointsAsync(entityView);

        _sequenceDisplayCompleted.OnNext(Unit.Default);
    }

    private async UniTask DisplayPointsAsync(BirdEntityView entityView)
    {
        Bounds birdBounds = entityView.GetComponent<Collider>().bounds;
        Vector3 topCenter = new(birdBounds.center.x, birdBounds.max.y, birdBounds.center.z);
        _pointsDisplayStarted.OnNext(new BirdPointsDisplayData(topCenter, entityView.PointsSettings));

        await UniTask.WaitForSeconds(entityView.PointsSettings.TotalDuration, cancellationToken: _cts.Token);
    }
}
