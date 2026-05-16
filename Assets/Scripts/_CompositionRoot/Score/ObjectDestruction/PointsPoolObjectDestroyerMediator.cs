using R3;
using UnityEngine;

public class PointsPoolObjectDestroyerMediator : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool;
    private readonly ObjectDestroyer _destroyer;

    public PointsPoolObjectDestroyerMediator(PointsObjectPool pointsObjectPool,
        ObjectDestroyer destroyer)
    {
        _pointsObjectPool = pointsObjectPool;
        _destroyer = destroyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(disposables);
    }

    private void OnDestroyed(DestructionEvent destructionEvent)
    {
        ObjectDestroyerView destroyerView = destructionEvent.DestroyerView;
        Vector3 position = destroyerView.transform.position;
        PointsSettings pointsSettings = destroyerView.Settings.PointsSettings;
        _pointsObjectPool.ShowPoints(position, pointsSettings);
    }
}
