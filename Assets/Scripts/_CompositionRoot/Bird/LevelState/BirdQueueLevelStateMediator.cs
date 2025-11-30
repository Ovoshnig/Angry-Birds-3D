using R3;
using VContainer.Unity;

public class BirdQueueLevelStateMediator : Mediator, IPostInitializable
{
    private readonly BirdQueue _birdQueue;
    private readonly LevelStateLogic _levelStateLogic;

    public BirdQueueLevelStateMediator(BirdQueue birdQueue, LevelStateLogic levelStateLogic)
    {
        _birdQueue = birdQueue;
        _levelStateLogic = levelStateLogic;
    }

    public override void Initialize()
    {
    }

    public void PostInitialize()
    {
        _levelStateLogic.State
            .Where(s => s == LevelState.Idle)
            .Subscribe(_ => _birdQueue.DequeueBird())
            .AddTo(CompositeDisposable);
    }
}
