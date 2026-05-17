using R3;

public class SFXPlayerClearingPanelViewMediator : Mediator
{
    private readonly SFXPlayerObjectPool _sfxPlayer;
    private readonly ClearingPanelView _clearingPanelView;
    private readonly LevelSFXSettings _levelSFXSettings;

    public SFXPlayerClearingPanelViewMediator(SFXPlayerObjectPool sfxPlayer,
        ClearingPanelView clearingPanelView,
        LevelSFXSettings levelSFXSettings)
    {
        _sfxPlayer = sfxPlayer;
        _clearingPanelView = clearingPanelView;
        _levelSFXSettings = levelSFXSettings;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _clearingPanelView.Shown
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSFXSettings.ClearingPanelResource))
            .AddTo(disposables);
    }
}
