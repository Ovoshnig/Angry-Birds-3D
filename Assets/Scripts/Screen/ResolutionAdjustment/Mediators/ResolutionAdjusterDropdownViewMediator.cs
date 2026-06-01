using R3;

public class ResolutionAdjusterDropdownViewMediator : UIMediator<ResolutionAdjustDropdownView>
{
    private readonly ResolutionAdjuster _resolutionAdjuster;

    public ResolutionAdjusterDropdownViewMediator(ResolutionAdjuster resolutionAdjuster,
        ResolutionAdjustDropdownView view) : base(view) => _resolutionAdjuster = resolutionAdjuster;

    protected override void OnViewEnabled(ResolutionAdjustDropdownView view, CompositeDisposable viewDisposables)
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
