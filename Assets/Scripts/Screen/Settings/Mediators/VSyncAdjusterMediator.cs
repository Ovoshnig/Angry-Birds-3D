using R3;

public class VSyncAdjusterMediator : UIMediator<VSyncToggleView>
{
    private readonly VSyncAdjuster _vSyncAdjuster;

    public VSyncAdjusterMediator(VSyncAdjuster vSyncAdjuster, VSyncToggleView view)
        : base(view) => _vSyncAdjuster = vSyncAdjuster;

    protected override void OnViewEnabled(VSyncToggleView view, CompositeDisposable viewDisposables)
    {
        _vSyncAdjuster.IsVSyncEnabled
            .Subscribe(view.SetIsOnWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(isOn =>
            {
                if (isOn)
                    _vSyncAdjuster.EnableVSync();
                else
                    _vSyncAdjuster.DisableVSync();
            })
            .AddTo(viewDisposables);
    }
}
