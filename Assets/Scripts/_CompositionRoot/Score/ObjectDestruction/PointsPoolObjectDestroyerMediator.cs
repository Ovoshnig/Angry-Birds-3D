using AngryBirds3D.LevelScore.Points;
using Ovoshnig.Mediation;
using Ovoshnig.ObjectDestruction.Destruction;
using Ovoshnig.ObjectDestruction.Entity;
using R3;
using UnityEngine;

namespace AngryBirds3D.Composition;

public class PointsPoolObjectDestroyerMediator(PointsObjectPool pointsObjectPool,
    ObjectDestroyer destroyer) : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool = pointsObjectPool;
    private readonly ObjectDestroyer _destroyer = destroyer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(disposables);
    }

    private void OnDestroyed(DestructibleEntityView entityView)
    {
        Vector3 position = entityView.transform.position;
        PointsSettings pointsSettings = entityView.DestructionProfile.PointsSettings;
        _pointsObjectPool.ShowPoints(position, pointsSettings);
    }
}
