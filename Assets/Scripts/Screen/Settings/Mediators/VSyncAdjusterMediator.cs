using R3;

public class VSyncAdjusterMediator : UIMediator<VSyncToggleView>
{
    private readonly VSyncAdjuster _vSyncAdjuster;

    public VSyncAdjusterMediator(VSyncAdjuster vSyncAdjuster, VSyncToggleView view)
        : base(view) => _vSyncAdjuster = vSyncAdjuster;

    protected override void OnViewEnabled(VSyncToggleView view, CompositeDisposable viewDisposables)
    {
        _vSyncAdjuster.IsVSync
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_vSyncAdjuster.SetVSync)
            .AddTo(viewDisposables);
    }
}
