public sealed class SettingsStorage : DataStorage
{
    public override DataStorageType StorageType => DataStorageType.Settings;

    protected override string FileName => SettingsConstants.FileName;
}
