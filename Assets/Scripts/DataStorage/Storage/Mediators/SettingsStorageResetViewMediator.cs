using R3;

public class SettingsStorageResetViewMediator : UIMediator<SettingsResetButtonView>
{
    private readonly SettingsStorage _settingsStorage;
    private readonly SettingsResetButtonView _settingsResetButtonView;

    public SettingsStorageResetViewMediator(SettingsStorage settingsStorage,
        SettingsResetButtonView settingsResetButtonView) : base(settingsResetButtonView)
    {
        _settingsStorage = settingsStorage;
        _settingsResetButtonView = settingsResetButtonView;
    }

    protected override void OnViewEnabled()
    {
        _settingsResetButtonView.Clicked
            .Subscribe(_ => _settingsStorage.ResetData())
            .AddTo(ViewDisposables);
    }
}
