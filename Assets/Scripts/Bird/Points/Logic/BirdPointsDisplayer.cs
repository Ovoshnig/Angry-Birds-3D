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
    private readonly Subject<BirdPointsDisplayEvent> _displayStarted = new();
    private readonly Subject<Unit> _allDisplayed = new();
    private readonly CancellationTokenSource _cts = new();

    public BirdPointsDisplayer(BirdQueue birdQueue, BirdSettings birdSettings)
    {
        _birdQueue = birdQueue;
        _birdSettings = birdSettings;
    }

    public Observable<BirdPointsDisplayEvent> DisplayStarted => _displayStarted;
    public Observable<Unit> AllDisplayed => _allDisplayed;

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public async UniTask DisplayPointsAsync()
    {
        while (_birdQueue.TryDequeueBird(out BirdEntityView entityView))
        {
            Bounds birdBounds = entityView.GetComponent<Renderer>().bounds;
            Vector3 topCenter = new(birdBounds.center.x, birdBounds.max.y, birdBounds.center.z);
            _displayStarted.OnNext(new BirdPointsDisplayEvent(topCenter, entityView.PointsSettings));

            await UniTask.WaitForSeconds(_birdSettings.PointsDisplayDelay, cancellationToken: _cts.Token);
        }

        _allDisplayed.OnNext(Unit.Default);
    }
}
