using Ovoshnig.Audio.SFX.Count;
using Ovoshnig.Audio.SFX.Playing;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Audio.SFX
{
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
}
