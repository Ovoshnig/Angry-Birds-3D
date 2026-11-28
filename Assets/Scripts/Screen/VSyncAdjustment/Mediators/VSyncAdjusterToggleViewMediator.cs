using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Screen.VSyncAdjustment;

public class VSyncAdjusterToggleViewMediator(VSyncAdjuster vSyncAdjuster, VSyncAdjustToggleView view)
    : UIViewMediator<VSyncAdjustToggleView>(view)
{
    private readonly VSyncAdjuster _vSyncAdjuster = vSyncAdjuster;

    protected override void OnViewEnabled(VSyncAdjustToggleView view, CompositeDisposable viewDisposables)
    {
        _vSyncAdjuster.IsVSync
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_vSyncAdjuster.SetVSync)
            .AddTo(viewDisposables);
    }
}
