using R3;

public class BirdFlyer
{
    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(_ => birdEntityView.FlyerView.LookAtVelocityDirection())
            .RegisterTo(birdEntityView.destroyCancellationToken);
    }
}
