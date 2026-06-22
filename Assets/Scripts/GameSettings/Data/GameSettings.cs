using UnityEngine;

[CreateAssetMenu(fileName = nameof(GameSettings),
    menuName = "Scriptable Objects/Game Settings")]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public SceneSettings SceneSettings { get; private set; }
    [field: SerializeField] public CameraSettings CameraSettings { get; private set; }
    [field: SerializeField] public AudioSettings AudioSettings { get; private set; }
    [field: SerializeField] public TrailParticleSettings TrailParticleSettings { get; private set; }
    [field: SerializeField] public SkyboxSettings SkyboxSettings { get; private set; }
    [field: SerializeField] public CollisionSettings CollisionSettings { get; private set; }
    [field: SerializeField] public SlingshotSettings SlingshotSettings { get; private set; }
    [field: SerializeField] public SlingshotPlacingSettings SlingshotPlacingSettings { get; private set; }
    [field: SerializeField] public BirdSettings BirdSettings { get; private set; }
    [field: SerializeField] public BirdStretchSettings BirdStretchSettings { get; private set; }
    [field: SerializeField] public BirdPowerSettings BirdPowerSettings { get; private set; }
    [field: SerializeField] public ScoreSettings ScoreSettings { get; private set; }
}
