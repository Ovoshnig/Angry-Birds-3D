using Ovoshnig.UI.Basic;
using Ovoshnig.UI.Input;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.UI.PanelSwitching
{
    public class InputProviderCloseButtonViewsMediator : UIViewsMediator<PanelCloseButtonView>
    {
        private readonly UIInputProvider _uiInputProvider;

        public InputProviderCloseButtonViewsMediator(UIInputProvider uIInputProvider, IReadOnlyList<PanelCloseButtonView> views)
            : base(views) => _uiInputProvider = uIInputProvider;

        protected override void OnViewEnabled(PanelCloseButtonView view, CompositeDisposable viewDisposables)
        {
            _uiInputProvider.CancelPressed
                .Pairwise()
                .Where(pressed => !pressed.Previous && pressed.Current)
                .Subscribe(_ => view.Switch())
                .AddTo(viewDisposables);
        }
    }
}
