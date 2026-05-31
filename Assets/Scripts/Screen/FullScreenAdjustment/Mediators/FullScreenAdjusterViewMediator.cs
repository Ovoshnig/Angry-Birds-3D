using R3;

public class FullScreenAdjusterViewMediator : UIMediator<FullScreenAdjusterView>
{
    private readonly FullScreenAdjuster _fullScreenAdjuster;

    public FullScreenAdjusterViewMediator(FullScreenAdjuster fullScreenAdjuster, FullScreenAdjusterView view)
        : base(view) => _fullScreenAdjuster = fullScreenAdjuster;

    protected override void OnViewEnabled(FullScreenAdjusterView view, CompositeDisposable viewDisposables)
    {
        _fullScreenAdjuster.IsFullScreen
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_fullScreenAdjuster.SetFullScreen)
            .AddTo(viewDisposables);
    }
}
