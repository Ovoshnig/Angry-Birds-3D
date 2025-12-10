using R3;
using UnityEngine;

public class PointsObjectPoolPigDestroyerMediator : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool;
    private readonly PigDestroyer _pigDestroyer;

    public PointsObjectPoolPigDestroyerMediator(PointsObjectPool pointsObjectPool,
        PigDestroyer pigDestroyer)
    {
        _pointsObjectPool = pointsObjectPool;
        _pigDestroyer = pigDestroyer;
    }

    public override void Initialize()
    {
        _pigDestroyer.Destroyed
             .Subscribe(OnDestroyed)
             .AddTo(CompositeDisposable);
    }

    private void OnDestroyed(PigDestructionEvent destructionEvent)
    {
        Vector3 position = destructionEvent.EntityView.transform.position;
        int points = destructionEvent.Points;
        _pointsObjectPool.ShowPoints(position, points);
    }
}
