using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "DestructionSfxProfile", menuName = "Scriptable Objects/Destruction/Sfx Profile")]
public class DestructionSfxProfile : ScriptableObject
{
    [field: SerializeField] public AudioResource GlidingResource { get; private set; }
    [field: SerializeField] public AudioResource CollisionResource { get; private set; }
    [field: SerializeField] public AudioResource DamageResource { get; private set; }
    [field: SerializeField] public AudioResource DestructionResource { get; private set; }
}
