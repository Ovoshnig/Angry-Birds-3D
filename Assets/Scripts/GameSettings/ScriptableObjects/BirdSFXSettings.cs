using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "BirdSFXSettings", menuName = "Scriptable Objects/BirdSFXSettings")]
public class BirdSFXSettings : ScriptableObject
{
    [field: SerializeField] public AudioResource SelectionResource { get; private set; }
    [field: SerializeField] public AudioResource FlyingResource { get; private set; }
    [field: SerializeField] public AudioResource CollisionResource { get; private set; }
    [field: SerializeField] public AudioResource DestructionResource { get; private set; }
}
