using R3;

public class SettingsStorageResetViewMediator : UIMediator<SettingsResetButtonView>
{
    private readonly SettingsStorage _settingsStorage;

    public SettingsStorageResetViewMediator(SettingsStorage settingsStorage, SettingsResetButtonView view)
        : base(view) => _settingsStorage = settingsStorage;

    protected override void OnViewEnabled(SettingsResetButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _settingsStorage.ResetData())
            .AddTo(viewDisposables);
    }
}
