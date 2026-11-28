using Ovoshnig.Mediation;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.UI.Basic;

public abstract class UIViewsMediator<TView>(IReadOnlyList<TView> views) : Mediator where TView : UIView
{
    private readonly IReadOnlyList<TView> _views = views;
    private readonly Dictionary<TView, CompositeDisposable> _viewToDisposables = [];

    protected override void Bind(CompositeDisposable disposables)
    {
        foreach (var view in _views)
        {
            CompositeDisposable viewDisposables = [];
            _viewToDisposables[view] = viewDisposables;

            view.IsEnabled
                .Subscribe(enabled =>
                {
                    if (enabled)
                        OnViewEnabled(view, viewDisposables);
                    else
                        OnViewDisabled(view);
                })
                .AddTo(disposables);
        }
    }

    protected override void Unbind()
    {
        base.Unbind();

        foreach (var kvp in _viewToDisposables)
            kvp.Value.Dispose();

        _viewToDisposables.Clear();
    }

    protected abstract void OnViewEnabled(TView view, CompositeDisposable viewDisposables);

    protected virtual void OnViewDisabled(TView view)
    {
        if (_viewToDisposables.TryGetValue(view, out CompositeDisposable viewDisposables))
            viewDisposables.Clear();
    }
}
