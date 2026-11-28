using Ovoshnig.Audio.Music.ClipLoading;
using Ovoshnig.Audio.Music.Playback;
using Ovoshnig.Audio.Music.Queue;
using Ovoshnig.Audio.Music.SceneMusicMapping;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Audio.Music;

public class AudioMusicInstaller(MusicPlayerView musicPlayerView) : IInstaller
{
    private readonly MusicPlayerView _musicPlayerView = musicPlayerView;

    public void Install(IContainerBuilder builder)
    {
        builder.Register<IClipLoader, AddressablesClipLoader>(Lifetime.Singleton);
        builder.Register<ISceneMusicMapper, SceneMusicMapper>(Lifetime.Singleton);
        builder.Register<MusicQueue<object>>(Lifetime.Singleton);
        builder.Register<MusicPlayer>(Lifetime.Singleton);

        builder.RegisterComponentInNewPrefab(_musicPlayerView, Lifetime.Singleton)
            .DontDestroyOnLoad();

        builder.RegisterEntryPoint<MusicPlayerMediator>();
    }
}
