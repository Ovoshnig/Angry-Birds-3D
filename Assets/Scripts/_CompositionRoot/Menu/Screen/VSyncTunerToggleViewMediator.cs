using R3;

public class VSyncTunerToggleViewMediator : Mediator
{
    private readonly VSyncTuner _vSyncTuner;
    private readonly VSyncToggleView _vSyncToggleView;

    public VSyncTunerToggleViewMediator(VSyncTuner vSyncTuner,
        VSyncToggleView vSyncToggleView)
    {
        _vSyncTuner = vSyncTuner;
        _vSyncToggleView = vSyncToggleView;
    }

    public override void Start()
    {
        _vSyncTuner.IsVSyncEnabled
            .Subscribe(_vSyncToggleView.SetIsOnWithoutNotify)
            .AddTo(CompositeDisposable);

        _vSyncToggleView.ValueChanged
            .Subscribe(isOn =>
            {
                if (isOn)
                    _vSyncTuner.EnableVSync();
                else
                    _vSyncTuner.DisableVSync();
            })
            .AddTo(CompositeDisposable);
    }
}
