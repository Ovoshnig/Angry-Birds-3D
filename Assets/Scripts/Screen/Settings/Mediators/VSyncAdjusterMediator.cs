using R3;

public class VSyncAdjusterMediator : UIMediator<VSyncToggleView>
{
    private readonly VSyncAdjuster _vSyncAdjuster;
    private readonly VSyncToggleView _vSyncToggleView;

    public VSyncAdjusterMediator(VSyncAdjuster vSyncAdjuster,
        VSyncToggleView vSyncToggleView) : base(vSyncToggleView)
    {
        _vSyncAdjuster = vSyncAdjuster;
        _vSyncToggleView = vSyncToggleView;
    }

    protected override void OnViewEnabled()
    {
        _vSyncAdjuster.IsVSyncEnabled
            .Subscribe(_vSyncToggleView.SetIsOnWithoutNotify)
            .AddTo(Disposables);

        _vSyncToggleView.ValueChanged
            .Subscribe(isOn =>
            {
                if (isOn)
                    _vSyncAdjuster.EnableVSync();
                else
                    _vSyncAdjuster.DisableVSync();
            })
            .AddTo(Disposables);
    }
}
