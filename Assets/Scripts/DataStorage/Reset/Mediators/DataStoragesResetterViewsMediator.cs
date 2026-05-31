using R3;
using System.Collections.Generic;
using System.Linq;

public class DataStoragesResetterViewsMediator : UIListMediator<DataResetterView>
{
    private readonly IReadOnlyList<DataStorage> _dataStorages;

    public DataStoragesResetterViewsMediator(IReadOnlyList<DataStorage> dataStorages,
        IReadOnlyList<DataResetterView> views) : base(views) =>
        _dataStorages = dataStorages;

    protected override void OnViewEnabled(DataResetterView view, CompositeDisposable viewDisposables)
    {
        DataStorage dataStorage = _dataStorages.FirstOrDefault(s => s.StorageType == view.StorageType);

        if (dataStorage == null)
            return;

        view.Clicked
            .Subscribe(_ => dataStorage.ResetData())
            .AddTo(viewDisposables);
    }
}
