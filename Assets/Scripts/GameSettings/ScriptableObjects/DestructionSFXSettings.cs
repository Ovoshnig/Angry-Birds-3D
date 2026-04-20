using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "DestructionSFXSettings", 
    menuName = "Scriptable Objects/DestructionSettings/SFX")]
public class DestructionSFXSettings : ScriptableObject
{
    [field: SerializeField] public AudioResource GlidingResource { get; private set; }
    [field: SerializeField] public AudioResource CollisionResource { get; private set; }
    [field: SerializeField] public AudioResource DamageResource { get; private set; }
    [field: SerializeField] public AudioResource DestructionResource { get; private set; }
}
