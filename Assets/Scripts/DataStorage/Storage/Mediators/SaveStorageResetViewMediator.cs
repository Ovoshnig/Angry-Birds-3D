using R3;

public class SaveStorageResetViewMediator : UIMediator<SaveResetButtonView>
{
    private readonly SaveStorage _saveStorage;

    public SaveStorageResetViewMediator(SaveStorage saveStorage, SaveResetButtonView view)
        : base(view) => _saveStorage = saveStorage;

    protected override void OnViewEnabled(SaveResetButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _saveStorage.ResetData())
            .AddTo(viewDisposables);
    }
}
