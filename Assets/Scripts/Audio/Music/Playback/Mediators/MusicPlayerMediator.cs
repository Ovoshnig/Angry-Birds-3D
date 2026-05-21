using R3;

public class MusicPlayerMediator : Mediator
{
    private readonly MusicPlayer _musicPlayer;
    private readonly MusicPlayerView _musicPlayerView;

    public MusicPlayerMediator(MusicPlayer musicPlayer, MusicPlayerView musicPlayerView)
    {
        _musicPlayer = musicPlayer;
        _musicPlayerView = musicPlayerView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _musicPlayer.PlaybackStarted
            .Subscribe(clip => _musicPlayerView.Play(clip))
            .AddTo(disposables);

        _musicPlayer.PlaybackEnded
            .Subscribe(_ => _musicPlayerView.Stop())
            .AddTo(disposables);
    }
}
