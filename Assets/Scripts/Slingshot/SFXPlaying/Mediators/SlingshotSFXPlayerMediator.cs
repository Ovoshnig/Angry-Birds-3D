using R3;

public class SlingshotSFXPlayerMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly SlingshotSFXPlayerView _slingshotSFXPlayerView;

    public SlingshotSFXPlayerMediator(SlingshotShooter slingshotShooter,
        SlingshotSFXPlayerView slingshotSFXPlayerView)
    {
        _slingshotShooter = slingshotShooter;
        _slingshotSFXPlayerView = slingshotSFXPlayerView;
    }

    public override void Initialize()
    {
        _slingshotShooter.DraggingStarted
            .Subscribe(_ => _slingshotSFXPlayerView.PlayDragging())
            .AddTo(CompositeDisposable);

        _slingshotShooter.Shot
            .Subscribe(_ => _slingshotSFXPlayerView.PlayShot())
            .AddTo(CompositeDisposable);
    }
}
