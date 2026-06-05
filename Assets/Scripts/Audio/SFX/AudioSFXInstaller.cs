using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class AudioSFXInstaller : IInstaller
{
    [SerializeField] private SFXPlayingInstaller _playingInstaller;
    [SerializeField] private SFXCountInstaller _sfxCountInstaller;

    public void Install(IContainerBuilder builder)
    {
        _playingInstaller.Install(builder);
        _sfxCountInstaller.Install(builder);
    }
}
