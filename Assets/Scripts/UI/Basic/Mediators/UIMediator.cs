using R3;

public abstract class UIMediator<TView> : Mediator where TView : UIView
{
    private readonly TView _view;

    public UIMediator(TView view) => _view = view;

    protected CompositeDisposable ViewDisposables { get; } = new();

    public override void Start()
    {
        _view.IsEnabled
            .Subscribe(enabled =>
            {
                if (enabled)
                    OnViewEnabled();
                else
                    OnViewDisabled();
            })
            .AddTo(Disposables);
    }

    public override void Dispose()
    {
        base.Dispose();
        ViewDisposables.Dispose();
    }

    protected abstract void OnViewEnabled();

    protected virtual void OnViewDisabled() => ViewDisposables.Clear();
}
