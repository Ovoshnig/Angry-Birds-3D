using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;

public record BirdPointsDisplayEvent(Vector3 Position, PointsSettings PointsSettings);

public class BirdPointsDisplayer : IDisposable
{
    private readonly BirdQueue _birdQueue;
    private readonly BirdSettings _birdSettings;
    private readonly Subject<BirdPointsDisplayEvent> _birdDisplayStarted = new();
    private readonly Subject<Unit> _birdSequenceDisplayCompleted = new();
    private readonly CancellationTokenSource _cts = new();

    private BirdEntityView _slingshotBird = null;

    public BirdPointsDisplayer(BirdQueue birdQueue, BirdSettings birdSettings)
    {
        _birdQueue = birdQueue;
        _birdSettings = birdSettings;
    }

    public Observable<BirdPointsDisplayEvent> BirdDisplayStarted => _birdDisplayStarted;
    public Observable<Unit> BirdSequenceDisplayCompleted => _birdSequenceDisplayCompleted;

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public void SetSlingshotBird(BirdEntityView slingshotBird) => _slingshotBird = slingshotBird;

    public async UniTask DisplayBirdSequenceAsync()
    {
        while (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
            await DisplayBirdAsync(entityView);

        if (_slingshotBird != null)
            await DisplayBirdAsync(_slingshotBird);

        _birdSequenceDisplayCompleted.OnNext(Unit.Default);
    }

    private async UniTask DisplayBirdAsync(BirdEntityView bird)
    {
        Bounds birdBounds = bird.GetComponent<Renderer>().bounds;
        Vector3 topCenter = new(birdBounds.center.x, birdBounds.max.y, birdBounds.center.z);
        _birdDisplayStarted.OnNext(new BirdPointsDisplayEvent(topCenter, bird.PointsSettings));

        await UniTask.WaitForSeconds(_birdSettings.PointsDisplayDelay, cancellationToken: _cts.Token);
    }
}
