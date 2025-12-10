using R3;
using UnityEngine;

public class PointsObjectPoolBlockDestroyerMediator : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool;
    private readonly BlockDestroyer _blockDestroyer;

    public PointsObjectPoolBlockDestroyerMediator(PointsObjectPool pointsObjectPool,
        BlockDestroyer blockDestroyer)
    {
        _pointsObjectPool = pointsObjectPool;
        _blockDestroyer = blockDestroyer;
    }

    public override void Initialize()
    {
        _blockDestroyer.Destroyed
             .Subscribe(OnDestroyed)
             .AddTo(CompositeDisposable);
    }

    private void OnDestroyed(BlockDestructionEvent destructionEvent)
    {
        Vector3 position = destructionEvent.EntityView.transform.position;
        int points = destructionEvent.Points;
        Color color = destructionEvent.Color;
        float fontSize = destructionEvent.FontSize;

        _pointsObjectPool.ShowPoints(position, points, color, fontSize);
    }
}
