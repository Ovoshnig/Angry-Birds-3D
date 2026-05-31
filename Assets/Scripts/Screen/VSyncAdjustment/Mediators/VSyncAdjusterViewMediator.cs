using R3;

public class VSyncAdjusterViewMediator : UIMediator<VSyncAdjusterView>
{
    private readonly VSyncAdjuster _vSyncAdjuster;

    public VSyncAdjusterViewMediator(VSyncAdjuster vSyncAdjuster, VSyncAdjusterView view)
        : base(view) => _vSyncAdjuster = vSyncAdjuster;

    protected override void OnViewEnabled(VSyncAdjusterView view, CompositeDisposable viewDisposables)
    {
        _vSyncAdjuster.IsVSync
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_vSyncAdjuster.SetVSync)
            .AddTo(viewDisposables);
    }
}
