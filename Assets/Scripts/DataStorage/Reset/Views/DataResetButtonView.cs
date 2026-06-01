using UnityEngine;

public class DataResetButtonView : ButtonView
{
    [field: SerializeField] public DataStorageType StorageType { get; private set; }
}
