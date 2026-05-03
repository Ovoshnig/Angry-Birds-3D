using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdEntityInstaller : IInstaller
{
    [SerializeField] private GameObject _birdQueue;

    public void Install(IContainerBuilder builder)
    {
        BirdEntityView[] entityViews = _birdQueue.GetComponentsInChildren<BirdEntityView>();
        builder.RegisterInstance(entityViews);
    }
}
