using R3;
using System.Collections.Generic;

public abstract class UIListMediator<TView> : Mediator where TView : UIView
{
    private readonly IReadOnlyList<TView> _views;
    private readonly Dictionary<TView, CompositeDisposable> _viewToDisposables = new();

    public UIListMediator(IReadOnlyList<TView> views) => _views = views;

    public override void Start()
    {
        foreach (var view in _views)
        {
            CompositeDisposable viewDisposables = new();
            _viewToDisposables[view] = viewDisposables;

            view.IsEnabled
                .Subscribe(enabled =>
                {
                    if (enabled)
                        OnViewEnabled(view, viewDisposables);
                    else
                        OnViewDisabled(view);
                })
                .AddTo(Disposables);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

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
