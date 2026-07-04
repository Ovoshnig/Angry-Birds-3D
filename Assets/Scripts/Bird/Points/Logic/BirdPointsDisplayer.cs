using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;

public class BirdPointsDisplayer : IDisposable
{
    private readonly BirdQueue _birdQueue;
    private readonly Subject<BirdPointsDisplayData> _pointsDisplayStarted = new();
    private readonly Subject<Unit> _sequenceDisplayCompleted = new();
    private readonly CancellationTokenSource _cts = new();

    private BirdEntityView _slingshotBird = null;

    public BirdPointsDisplayer(BirdQueue birdQueue) => _birdQueue = birdQueue;

    public Observable<BirdPointsDisplayData> PointsDisplayStarted => _pointsDisplayStarted;
    public Observable<Unit> SequenceDisplayCompleted => _sequenceDisplayCompleted;

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public void SetSlingshotBird(BirdEntityView slingshotBird) => _slingshotBird = slingshotBird;

    public async UniTask DisplaySequenceAsync()
    {
        while (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
            await DisplayPointsAsync(entityView);

        if (_slingshotBird != null)
            await DisplayPointsAsync(_slingshotBird);

        _sequenceDisplayCompleted.OnNext(Unit.Default);
    }

    private async UniTask DisplayPointsAsync(BirdEntityView bird)
    {
        Bounds birdBounds = bird.GetComponent<Collider>().bounds;
        Vector3 topCenter = new(birdBounds.center.x, birdBounds.max.y, birdBounds.center.z);
        _pointsDisplayStarted.OnNext(new BirdPointsDisplayData(topCenter, bird.PointsSettings));

        await UniTask.WaitForSeconds(bird.PointsSettings.TotalDuration, cancellationToken: _cts.Token);
    }
}
