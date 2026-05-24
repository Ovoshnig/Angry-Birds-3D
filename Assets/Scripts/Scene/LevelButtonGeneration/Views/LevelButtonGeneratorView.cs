using UnityEngine;

public class LevelButtonGeneratorView : MonoBehaviour
{
    [field: SerializeField] public RectTransform LevelButtonParent { get; private set; }
    [field: SerializeField] public SceneButtonView LevelButtonPrefab { get; private set; }
    [field: SerializeField] public GameSettings GameSettings { get; private set; }
}
