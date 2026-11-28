using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Screen.FullScreenAdjustment;

public class FullScreenAdjusterToggleViewMediator(FullScreenAdjuster fullScreenAdjuster,
    FullScreenAdjustToggleView view) : UIViewMediator<FullScreenAdjustToggleView>(view)
{
    private readonly FullScreenAdjuster _fullScreenAdjuster = fullScreenAdjuster;

    protected override void OnViewEnabled(FullScreenAdjustToggleView view,
        CompositeDisposable viewDisposables)
    {
        _fullScreenAdjuster.IsFullScreen
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_fullScreenAdjuster.SetFullScreen)
            .AddTo(viewDisposables);
    }
}
