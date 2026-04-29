using R3;
using UnityEngine;

public class SlingshotShooterBirdQueueMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly BirdQueue _birdQueue;

    public SlingshotShooterBirdQueueMediator(SlingshotShooter slingshotShooter,
        BirdQueue birdQueue)
    {
        _slingshotShooter = slingshotShooter;
        _birdQueue = birdQueue;
    }

    public override void Start()
    {
        _birdQueue.BirdDequeued
            .Subscribe(birdEntityView =>
            {
                Rigidbody birdRigidbody = birdEntityView.FlyerView.Rigidbody;
                _slingshotShooter.SetCurrentBird(birdRigidbody);
            })
            .AddTo(CompositeDisposable);

        _birdQueue.TryDequeueBird();
    }
}
