using R3;
using UnityEngine;

public class PointsPoolObjectDestroyerMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly PointsObjectPool _pointsObjectPool;
    private readonly ObjectDestroyer<TView> _destroyer;

    public PointsPoolObjectDestroyerMediator(PointsObjectPool pointsObjectPool,
        ObjectDestroyer<TView> destroyer)
    {
        _pointsObjectPool = pointsObjectPool;
        _destroyer = destroyer;
    }

    public override void Initialize() =>
        _destroyer.Destroyed.Subscribe(OnDestroyed).AddTo(CompositeDisposable);

    private void OnDestroyed(DestructionEvent<TView> destructionEvent)
    {
        ObjectDestroyerView destroyerView = destructionEvent.DestroyerView;
        Vector3 position = destroyerView.transform.position;
        DestructionPointsSettings pointsSettings = destroyerView.Settings.PointsSettings;
        _pointsObjectPool.ShowPoints(position, pointsSettings);
    }
}
