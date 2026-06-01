using R3;

public class VSyncAdjusterToggleViewMediator : UIViewMediator<VSyncAdjustToggleView>
{
    private readonly VSyncAdjuster _vSyncAdjuster;

    public VSyncAdjusterToggleViewMediator(VSyncAdjuster vSyncAdjuster, VSyncAdjustToggleView view)
        : base(view) => _vSyncAdjuster = vSyncAdjuster;

    protected override void OnViewEnabled(VSyncAdjustToggleView view, CompositeDisposable viewDisposables)
    {
        _vSyncAdjuster.IsVSync
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(_vSyncAdjuster.SetVSync)
            .AddTo(viewDisposables);
    }
}
