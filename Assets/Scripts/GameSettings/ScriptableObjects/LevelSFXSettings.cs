using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "LevelSFXSettings", menuName = "Scriptable Objects/LevelSFXSettings")]
public class LevelSFXSettings : ScriptableObject
{
    [field: SerializeField] public AudioResource StartResource { get; private set; }
    [field: SerializeField] public AudioResource NextResource { get; private set; }
    [field: SerializeField] public AudioResource ClearingResource { get; private set; }
    [field: SerializeField] public AudioResource ClearingPanelResource { get; private set; }
    [field: SerializeField] public AudioResource FailureResource { get; private set; }
}
