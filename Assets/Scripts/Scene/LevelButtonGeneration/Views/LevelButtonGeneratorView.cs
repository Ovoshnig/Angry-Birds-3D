using UnityEngine;
using GameSettingsAsset = Ovoshnig.GameSettings.GameSettings;
using GameSettings = Ovoshnig.GameSettings.GameSettings;

namespace Ovoshnig.Scene.LevelButtonGeneration
{
    public class LevelButtonGeneratorView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform LevelButtonParent { get; private set; }
        [field: SerializeField] public RectTransform LevelButtonBlockPrefab { get; private set; }
        [field: SerializeField] public GameSettingsAsset GameSettings { get; private set; }
    }
}
