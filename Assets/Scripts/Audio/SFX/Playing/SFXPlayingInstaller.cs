using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Audio.SFX.Playing
{
    [Serializable]
    public class SFXPlayingInstaller : IInstaller
    {
        [SerializeField] private SFXPlayerView _sfxPlayerViewPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sfxPlayerViewPrefab);
            builder.Register<SFXPlayerObjectPool>(Lifetime.Singleton);
        }
    }
}
