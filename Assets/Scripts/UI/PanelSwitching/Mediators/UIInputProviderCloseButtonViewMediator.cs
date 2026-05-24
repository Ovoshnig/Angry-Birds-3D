using R3;
using System.Collections.Generic;

public class UIInputProviderCloseButtonViewMediator : UIListMediator<PanelCloseButtonView>
{
    private readonly UIInputProvider _uiInputProvider;

    public UIInputProviderCloseButtonViewMediator(UIInputProvider uIInputProvider, IReadOnlyList<PanelCloseButtonView> views)
        : base(views) => _uiInputProvider = uIInputProvider;

    protected override void OnViewEnabled(PanelCloseButtonView view, CompositeDisposable viewDisposables)
    {
        _uiInputProvider.CloseCurrentPressed
            .Pairwise()
            .Where(pressed => !pressed.Previous && pressed.Current)
            .Subscribe(_ => view.Switch())
            .AddTo(viewDisposables);
    }
}
