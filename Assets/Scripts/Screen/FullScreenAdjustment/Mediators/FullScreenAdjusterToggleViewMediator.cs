using R3;

public class FullScreenAdjusterToggleViewMediator : UIMediator<FullScreenAdjustToggleView>
{
    private readonly FullScreenAdjuster _fullScreenAdjuster;

    public FullScreenAdjusterToggleViewMediator(FullScreenAdjuster fullScreenAdjuster, FullScreenAdjustToggleView view)
        : base(view) => _fullScreenAdjuster = fullScreenAdjuster;

    protected override void OnViewEnabled(FullScreenAdjustToggleView view, CompositeDisposable viewDisposables)
    {
        _fullScreenAdjuster.IsFullScreen
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_fullScreenAdjuster.SetFullScreen)
            .AddTo(viewDisposables);
    }
}
