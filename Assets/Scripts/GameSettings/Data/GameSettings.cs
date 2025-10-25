using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings),
    menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public SceneSettings SceneSettings { get; private set; }
    [field: SerializeField] public AudioSettings AudioSettings { get; private set; }
}
