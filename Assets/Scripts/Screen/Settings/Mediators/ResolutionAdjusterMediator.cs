using R3;
using System.Linq;

public class ResolutionAdjusterMediator : UIMediator<ResolutionDropdownView>
{
    private readonly ResolutionAdjuster _resolutionAdjuster;

    public ResolutionAdjusterMediator(ResolutionAdjuster resolutionAdjuster, ResolutionDropdownView view)
        : base(view) => _resolutionAdjuster = resolutionAdjuster;

    protected override void OnViewEnabled(ResolutionDropdownView view, CompositeDisposable viewDisposables)
    {
        view.SetResolutionOptions(_resolutionAdjuster.Resolutions.Select(r => r.ToString()).ToList());

        _resolutionAdjuster.CurrentResolutionIndex
            .Subscribe(view.SetValueWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_resolutionAdjuster.SetResolution)
            .AddTo(viewDisposables);
    }
}
