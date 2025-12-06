using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigDestructionInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        PigDestroyerView[] pigDestroyerViews = _blockStructure
            .GetComponentsInChildren<PigDestroyerView>();
        builder.RegisterInstance(pigDestroyerViews);
    }
}
