using System.Collections.Generic;
using VContainer.Unity;

public class RatingShower : IStartable
{
    private readonly SaveStorage _saveStorage;

    private Dictionary<int, int> _starRecordBylevelIndex;

    public RatingShower(SaveStorage saveStorage) => _saveStorage = saveStorage;

    public void Start() =>
        _starRecordBylevelIndex = _saveStorage.Get(SaveConstants.StarRecordBylevelIndex, new Dictionary<int, int>());

    public int GetStarRecord(int levelIndex) => _starRecordBylevelIndex.GetValueOrDefault(levelIndex, 0);
}
