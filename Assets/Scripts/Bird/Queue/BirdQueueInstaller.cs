using System;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdQueueInstaller : IInstaller
{
    [SerializeField] private GameObject _birdQueue;

    public void Install(IContainerBuilder builder)
    {
        BirdFlyerView[] birdFlyerViews = _birdQueue.GetComponentsInChildren<BirdFlyerView>().ToArray();
        builder.RegisterInstance(birdFlyerViews);

        builder.Register<BirdQueue>(Lifetime.Singleton);
    }
}
