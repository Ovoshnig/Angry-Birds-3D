using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdEntityInstaller : IInstaller
{
    [SerializeField] private GameObject _birdQueue;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<BirdEntityView> entityViews = _birdQueue.GetComponentsInChildren<BirdEntityView>();
        builder.RegisterInstance(entityViews);
    }
}
