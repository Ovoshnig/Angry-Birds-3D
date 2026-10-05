using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using System.Linq;
using DataStorageService = Ovoshnig.DataStorage.Storage.DataStorage;

namespace Ovoshnig.DataStorage.Reset
{
    public class DataStoragesResetButtonViewsMediator : UIViewsMediator<DataResetButtonView>
    {
        private readonly IReadOnlyList<DataStorageService> _dataStorages;

        public DataStoragesResetButtonViewsMediator(IReadOnlyList<DataStorageService> dataStorages,
            IReadOnlyList<DataResetButtonView> views) : base(views) =>
            _dataStorages = dataStorages;

        protected override void OnViewEnabled(DataResetButtonView view, CompositeDisposable viewDisposables)
        {
            DataStorageService dataStorage = _dataStorages.FirstOrDefault(s => s.StorageType == view.StorageType);

            if (dataStorage == null)
                return;

            view.Clicked
                .Subscribe(_ => dataStorage.ResetData())
                .AddTo(viewDisposables);
        }
    }
}
