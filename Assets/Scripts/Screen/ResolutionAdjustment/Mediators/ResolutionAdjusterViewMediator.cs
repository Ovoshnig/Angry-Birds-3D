using R3;

public class ResolutionAdjusterViewMediator : UIMediator<ResolutionAdjusterView>
{
    private readonly ResolutionAdjuster _resolutionAdjuster;

    public ResolutionAdjusterViewMediator(ResolutionAdjuster resolutionAdjuster, ResolutionAdjusterView view)
        : base(view) => _resolutionAdjuster = resolutionAdjuster;

    protected override void OnViewEnabled(ResolutionAdjusterView view, CompositeDisposable viewDisposables)
    {
        view.SetOptions(_resolutionAdjuster.Resolutions);

        _resolutionAdjuster.CurrentResolutionIndex
            .Subscribe(view.SetValueWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_resolutionAdjuster.SetResolution)
            .AddTo(viewDisposables);
    }
}
