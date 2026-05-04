using R3;
using System.Linq;

public class ResolutionAdjusterMediator : Mediator
{
    private readonly ResolutionAdjuster _resolutionAdjuster;
    private readonly ResolutionDropdownView _resolutionDropdownView;

    public ResolutionAdjusterMediator(ResolutionAdjuster resolutionAdjuster,
        ResolutionDropdownView resolutionDropdownView)
    {
        _resolutionAdjuster = resolutionAdjuster;
        _resolutionDropdownView = resolutionDropdownView;
    }

    public override void Start()
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
