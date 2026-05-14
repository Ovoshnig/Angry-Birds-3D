using R3;

public class FullScreenAdjusterMediator : UIMediator<FullScreenToggleView>
{
    private readonly FullScreenAdjuster _fullScreenAdjuster;
    private readonly FullScreenToggleView _fullScreenToggleView;

    public FullScreenAdjusterMediator(FullScreenAdjuster fullScreenAdjuster,
        FullScreenToggleView fullScreenToggleView) : base(fullScreenToggleView)
    {
        _fullScreenAdjuster = fullScreenAdjuster;
        _fullScreenToggleView = fullScreenToggleView;
    }

    protected override void OnViewEnabled()
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
