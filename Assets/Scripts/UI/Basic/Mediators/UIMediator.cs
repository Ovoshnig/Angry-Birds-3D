using R3;

public abstract class UIMediator<TView> : Mediator where TView : UIView
{
    private readonly TView _view;
    private readonly CompositeDisposable _viewDisposables = new();

    public UIMediator(TView view) => _view = view;

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
