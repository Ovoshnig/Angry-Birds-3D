using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings),
    menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public SceneSettings SceneSettings { get; private set; }
    [field: SerializeField] public AudioSettings AudioSettings { get; private set; }
    [field: SerializeField] public CollisionSettings CollisionSettings { get; private set; }
    [field: SerializeField] public SlingshotSettings SlingshotSettings { get; private set; }
    [field: SerializeField] public BirdSettings BirdSettings { get; private set; }
    [field: SerializeField] public ScoreSettings ScoreSettings { get; private set; }
}
