using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "BirdSfxProfile", menuName = "Scriptable Objects/Bird Sfx Profile")]
public class BirdSfxProfile : ScriptableObject
{
    [field: SerializeField] public AudioResource SelectionResource { get; private set; }
    [field: SerializeField] public AudioResource FlyingResource { get; private set; }
    [field: SerializeField] public AudioResource PowerActivationResource { get; private set; }
    [field: SerializeField] public AudioResource CollisionResource { get; private set; }
    [field: SerializeField] public AudioResource DestructionResource { get; private set; }
    [field: SerializeField] public AudioResource ExplosionResource { get; private set; }
}
