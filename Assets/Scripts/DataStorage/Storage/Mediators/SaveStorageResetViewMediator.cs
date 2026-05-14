using R3;

public class SaveStorageResetViewMediator : UIMediator<SaveResetButtonView>
{
    private readonly SaveStorage _saveStorage;
    private readonly SaveResetButtonView _saveResetButtonView;

    public SaveStorageResetViewMediator(SaveStorage saveStorage,
        SaveResetButtonView saveResetButtonView) : base(saveResetButtonView)
    {
        _saveStorage = saveStorage;
        _saveResetButtonView = saveResetButtonView;
    }

    protected override void OnViewEnabled()
    {
        _saveResetButtonView.Clicked
            .Subscribe(_ => _saveStorage.ResetData())
            .AddTo(Disposables);
    }
}
