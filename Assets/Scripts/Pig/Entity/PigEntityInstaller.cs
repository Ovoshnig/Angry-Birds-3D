using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigEntityInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<PigEntityView> pigEntityViews = _blockStructure.GetComponentsInChildren<PigEntityView>();
        builder.RegisterInstance(pigEntityViews);
    }
}
