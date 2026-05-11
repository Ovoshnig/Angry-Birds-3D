using R3;
using UnityEngine;

public class SlingshotShooterBirdDestroyerMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly BirdQueue _birdQueue;
    private readonly PigTracker _pigTracker;

    public SlingshotShooterBirdDestroyerMediator(SlingshotShooter slingshotShooter,
        BirdDestroyer birdDestroyer,
        BirdQueue birdQueue,
        PigTracker pigTracker)
    {
        _slingshotShooter = slingshotShooter;
        _birdDestroyer = birdDestroyer;
        _birdQueue = birdQueue;
        _pigTracker = pigTracker;
    }

    public override void Start()
    {
        _birdDestroyer.Destroyed
            .Subscribe(_ =>
            {
                if (_pigTracker.PigCount.CurrentValue > 0)
                    TryPutBirdInSlingshot();
            })
            .AddTo(Disposables);

        TryPutBirdInSlingshot();
    }

    private bool TryPutBirdInSlingshot()
    {
        if (!_birdQueue.TryDequeueBird(out BirdEntityView birdEntityView))
            return false;

        Rigidbody birdRigidbody = birdEntityView.FlyerView.Rigidbody;
        _slingshotShooter.SetCurrentBird(birdRigidbody);
        return true;
    }
}
