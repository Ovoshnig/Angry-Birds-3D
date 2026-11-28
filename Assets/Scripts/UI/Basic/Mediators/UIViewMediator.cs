using Ovoshnig.Mediation;
using R3;

namespace Ovoshnig.UI.Basic;

public abstract class UIViewMediator<TView>(TView view) : Mediator where TView : UIView
{
    private readonly TView _view = view;
    private readonly CompositeDisposable _viewDisposables = [];

    protected override void Bind(CompositeDisposable disposables)
    {
        _view.IsEnabled
            .Subscribe(enabled =>
            {
                if (enabled)
                    OnViewEnabled(_view, _viewDisposables);
                else
                    OnViewDisabled(_view);
            })
            .AddTo(disposables);
    }

    protected override void Unbind()
    {
        base.Unbind();
        _viewDisposables.Dispose();
    }

    protected abstract void OnViewEnabled(TView view, CompositeDisposable viewDisposables);

    protected virtual void OnViewDisabled(TView view) => _viewDisposables.Clear();
}
