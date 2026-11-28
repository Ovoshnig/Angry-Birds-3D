using Ovoshnig.Mediation;
using R3;

namespace Ovoshnig.Audio.Music.Playback;

public class MusicPlayerMediator(MusicPlayer musicPlayer, MusicPlayerView musicPlayerView) : Mediator
{
    private readonly MusicPlayer _musicPlayer = musicPlayer;
    private readonly MusicPlayerView _musicPlayerView = musicPlayerView;

    protected override void Bind(CompositeDisposable disposables)
    {
        _musicPlayer.PlaybackStarted
            .Subscribe(_musicPlayerView.Play)
            .AddTo(disposables);

        _musicPlayer.PlaybackEnded
            .Subscribe(_ => _musicPlayerView.Stop())
            .AddTo(disposables);
    }
}
