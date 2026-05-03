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

    public override void Start()
    {
        _musicPlayer.PlaybackStarted
            .Subscribe(clip =>
            {
                _musicPlayerView.SetClip(clip);
                _musicPlayerView.Play();
            })
            .AddTo(Disposables);
        _musicPlayer.PlaybackEnded
            .Subscribe(_ =>
            {
                _musicPlayerView.SetClip(null);
                _musicPlayerView.Stop();
            })
            .AddTo(Disposables);
    }
}
