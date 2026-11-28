using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Screen.ResolutionAdjustment;

public class ResolutionAdjusterDropdownViewMediator(ResolutionAdjuster resolutionAdjuster,
    ResolutionAdjustDropdownView view) : UIViewMediator<ResolutionAdjustDropdownView>(view)
{
    private readonly ResolutionAdjuster _resolutionAdjuster = resolutionAdjuster;

    protected override void OnViewEnabled(ResolutionAdjustDropdownView view,
        CompositeDisposable viewDisposables)
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
