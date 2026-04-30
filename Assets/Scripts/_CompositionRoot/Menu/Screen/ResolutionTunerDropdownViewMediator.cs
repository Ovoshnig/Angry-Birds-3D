using R3;
using System.Linq;

public class ResolutionTunerDropdownViewMediator : Mediator
{
    private readonly ResolutionTuner _resolutionTuner;
    private readonly ResolutionDropdownView _resolutionDropdownView;

    public ResolutionTunerDropdownViewMediator(ResolutionTuner resolutionTuner,
        ResolutionDropdownView resolutionDropdownView)
    {
        _resolutionTuner = resolutionTuner;
        _resolutionDropdownView = resolutionDropdownView;
    }

    public override void Start()
    {
        _resolutionDropdownView.SetResolutionOptions(_resolutionTuner.Resolutions.Select(r => r.ToString()).ToList());

        _resolutionTuner.CurrentResolutionIndex
            .Subscribe(_resolutionDropdownView.SetValueWithoutNotify)
            .AddTo(CompositeDisposable);

        _resolutionDropdownView.ValueChanged
            .Subscribe(_resolutionTuner.SetResolution)
            .AddTo(CompositeDisposable);
    }
}
