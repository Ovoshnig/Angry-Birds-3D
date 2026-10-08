using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Screen.FullScreenAdjustment
{
    public class FullScreenAdjusterToggleViewMediator : UIViewMediator<FullScreenAdjustToggleView>
    {
        private readonly FullScreenAdjuster _fullScreenAdjuster;

        public FullScreenAdjusterToggleViewMediator(FullScreenAdjuster fullScreenAdjuster, FullScreenAdjustToggleView view)
            : base(view) => _fullScreenAdjuster = fullScreenAdjuster;

        protected override void OnViewEnabled(FullScreenAdjustToggleView view, CompositeDisposable viewDisposables)
        {
            _fullScreenAdjuster.IsFullScreen
                .Subscribe(view.SetIsOnWithoutNotify)
                .AddTo(viewDisposables);

            view.ValueChanged
                .Subscribe(_fullScreenAdjuster.SetFullScreen)
                .AddTo(viewDisposables);
        }
    }
}
