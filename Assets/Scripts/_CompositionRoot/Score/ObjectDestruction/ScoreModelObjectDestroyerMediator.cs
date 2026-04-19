using R3;
using UnityEngine;

public class ScoreModelObjectDestroyerMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly ScoreModel _scoreModel;
    private readonly ObjectDestroyer<TView> _destroyer;

    public ScoreModelObjectDestroyerMediator(ScoreModel scoreModel, ObjectDestroyer<TView> destroyer)
    {
        _scoreModel = scoreModel;
        _destroyer = destroyer;
    }

    public override void Initialize()
    {
        _destroyer.Destroyed
            .Subscribe(destructionEvent =>
            _scoreModel.Increase(destructionEvent.DestroyerView.PointsSettings.Points))
            .AddTo(CompositeDisposable);
    }
}
