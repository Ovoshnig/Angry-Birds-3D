using Ovoshnig.DataStorage.Storage;
using Ovoshnig.UI.Basic;
using UnityEngine;

namespace Ovoshnig.DataStorage.Reset
{
    public class DataResetButtonView : ButtonView
    {
        [field: SerializeField] public DataStorageType StorageType { get; private set; }
    }
}
