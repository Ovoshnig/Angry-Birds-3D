using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PointsInstaller : IInstaller
{
    [SerializeField] private PointsView _pointsViewPrefab;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_pointsViewPrefab);
        builder.Register<PointsObjectPool>(Lifetime.Singleton);
    }
}
