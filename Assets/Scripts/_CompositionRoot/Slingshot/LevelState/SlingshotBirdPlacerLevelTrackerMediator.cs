using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.Queue;
using AngryBirds3D.Camera.Switching;
using AngryBirds3D.LevelState.Tracking;
using AngryBirds3D.Slingshot.Placement;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using R3;
using System.Threading;

namespace AngryBirds3D.Composition;

public class SlingshotBirdPlacerLevelTrackerMediator(SlingshotBirdPlacer slingshotBirdPlacer,
    LevelStateTracker levelStateTracker, BirdQueue birdQueue, CameraSwitchView cameraSwitchView) : Mediator
{
    private readonly SlingshotBirdPlacer _slingshotBirdPlacer = slingshotBirdPlacer;
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;
    private readonly BirdQueue _birdQueue = birdQueue;
    private readonly CameraSwitchView _cameraSwitchView = cameraSwitchView;

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.MovedToNext
            .SubscribeAwait(async (_, token) => await OnMovedToNextAsync(token), AwaitOperation.Drop)
            .AddTo(disposables);
    }

    private async UniTask OnMovedToNextAsync(CancellationToken token)
    {
        await UniTask.WaitWhile(() => _cameraSwitchView.IsBlending.CurrentValue, cancellationToken: token);

        if (_slingshotBirdPlacer.CanPlace && _birdQueue.TryDequeueBird(out BirdEntityView entityView))
            _slingshotBirdPlacer.PlaceBirdAsync(entityView.FlyerView.Rigidbody).Forget();
    }
}
