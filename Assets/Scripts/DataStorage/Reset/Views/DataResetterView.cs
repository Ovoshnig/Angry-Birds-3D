using UnityEngine;

public class DataResetterView : ButtonView
{
    [field: SerializeField] public DataStorageType StorageType { get; private set; }
}
