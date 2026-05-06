using R3;

public class FullScreenAdjusterMediator : Mediator
{
    private readonly FullScreenAdjuster _fullScreenAdjuster;
    private readonly FullScreenToggleView _fullScreenToggleView;

    public FullScreenAdjusterMediator(FullScreenAdjuster fullScreenAdjuster,
        FullScreenToggleView fullScreenToggleView)
    {
        _fullScreenAdjuster = fullScreenAdjuster;
        _fullScreenToggleView = fullScreenToggleView;
    }

    public override void Start()
    {
        _fullScreenAdjuster.IsFullScreen
            .Subscribe(_fullScreenToggleView.SetIsOnWithoutNotify)
            .AddTo(Disposables);

        _fullScreenToggleView.ValueChanged
            .Subscribe(isOn =>
            {
                if (isOn)
                    _fullScreenAdjuster.EnableFullScreen();
                else
                    _fullScreenAdjuster.DisableFullScreen();
            })
            .AddTo(Disposables);
    }
}
