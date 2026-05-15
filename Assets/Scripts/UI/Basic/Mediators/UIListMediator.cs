using R3;
using System.Collections.Generic;

public abstract class UIListMediator<TView> : Mediator where TView : UIView
{
    private readonly IReadOnlyList<TView> _views;

    public UIListMediator(IReadOnlyList<TView> views) => _views = views;

    protected Dictionary<TView, CompositeDisposable> ViewDisposables { get; } = new();

    public override void Start()
    {
        foreach (var view in _views)
        {
            CompositeDisposable disposables = new();
            ViewDisposables[view] = disposables;

            view.IsEnabled
                .Subscribe(enabled =>
                {
                    if (enabled)
                        OnViewEnabled(view, disposables);
                    else
                        OnViewDisabled(view);
                })
                .AddTo(Disposables);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        foreach (var kvp in ViewDisposables)
            kvp.Value.Dispose();

        ViewDisposables.Clear();
    }

    protected abstract void OnViewEnabled(TView view, CompositeDisposable disposables);

    protected virtual void OnViewDisabled(TView view)
    {
        if (ViewDisposables.TryGetValue(view, out CompositeDisposable disposables))
            disposables.Clear();
    }
}
