using System;
using UnityEngine;
using VContainer;

[Serializable]
public class PigEntityInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        PigEntityView[] pigEntityViews = _blockStructure
            .GetComponentsInChildren<PigEntityView>();
        builder.RegisterInstance(pigEntityViews);
    }
}
