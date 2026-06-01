using R3;
using System.Collections.Generic;
using System.Linq;

public class DataStoragesResetButtonViewsMediator : UIViewsMediator<DataResetButtonView>
{
    private readonly IReadOnlyList<DataStorage> _dataStorages;

    public DataStoragesResetButtonViewsMediator(IReadOnlyList<DataStorage> dataStorages,
        IReadOnlyList<DataResetButtonView> views) : base(views) =>
        _dataStorages = dataStorages;

    protected override void OnViewEnabled(DataResetButtonView view, CompositeDisposable viewDisposables)
    {
        DataStorage dataStorage = _dataStorages.FirstOrDefault(s => s.StorageType == view.StorageType);

        if (dataStorage == null)
            return;

        view.Clicked
            .Subscribe(_ => dataStorage.ResetData())
            .AddTo(viewDisposables);
    }
}
