using R3;
using System.Linq;

public class ResolutionAdjusterMediator : UIMediator<ResolutionDropdownView>
{
    private readonly ResolutionAdjuster _resolutionAdjuster;
    private readonly ResolutionDropdownView _resolutionDropdownView;

    public ResolutionAdjusterMediator(ResolutionAdjuster resolutionAdjuster,
        ResolutionDropdownView resolutionDropdownView) : base(resolutionDropdownView)
    {
        _resolutionAdjuster = resolutionAdjuster;
        _resolutionDropdownView = resolutionDropdownView;
    }

    protected override void OnViewEnabled()
    {
        _resolutionDropdownView.SetResolutionOptions(_resolutionAdjuster.Resolutions.Select(r => r.ToString()).ToList());

        _resolutionAdjuster.CurrentResolutionIndex
            .Subscribe(_resolutionDropdownView.SetValueWithoutNotify)
            .AddTo(Disposables);

        _resolutionDropdownView.ValueChanged
            .Subscribe(_resolutionAdjuster.SetResolution)
            .AddTo(Disposables);
    }
}
