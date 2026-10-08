using Ovoshnig.DataStorage.Storage;
using System.Collections.Generic;
using VContainer.Unity;

namespace AngryBirds3D.LevelScore.RatingShowing
{
    public class RatingShower : IStartable
    {
        private readonly SaveStorage _saveStorage;

        private Dictionary<int, int> _starRecordByLevelIndex;

        public RatingShower(SaveStorage saveStorage) => _saveStorage = saveStorage;

        public void Start() =>
            _starRecordByLevelIndex = _saveStorage.Get(SaveConstants.StarRecordByLevelIndex, new Dictionary<int, int>());

        public int GetStarRecord(int levelIndex) => _starRecordByLevelIndex.GetValueOrDefault(levelIndex, 0);
    }
}
