using R3;

public class VSyncAdjusterToggleViewMediator : Mediator
{
    private readonly VSyncAdjuster _vSyncAdjuster;
    private readonly VSyncToggleView _vSyncToggleView;

    public VSyncAdjusterToggleViewMediator(VSyncAdjuster vSyncAdjuster,
        VSyncToggleView vSyncToggleView)
    {
        _vSyncAdjuster = vSyncAdjuster;
        _vSyncToggleView = vSyncToggleView;
    }

    public override void Start()
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
