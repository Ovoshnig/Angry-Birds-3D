using R3;

public class FullScreenAdjusterMediator : UIMediator<FullScreenToggleView>
{
    private readonly FullScreenAdjuster _fullScreenAdjuster;

    public FullScreenAdjusterMediator(FullScreenAdjuster fullScreenAdjuster, FullScreenToggleView view)
        : base(view) => _fullScreenAdjuster = fullScreenAdjuster;

    protected override void OnViewEnabled(FullScreenToggleView view, CompositeDisposable viewDisposables)
    {
        _fullScreenAdjuster.IsFullScreen
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_fullScreenAdjuster.SetFullScreen)
            .AddTo(viewDisposables);
    }
}
