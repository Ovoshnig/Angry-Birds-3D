using UnityEngine;

[CreateAssetMenu(fileName = "BlockParticleProfile", menuName = "Scriptable Objects/Block Particle Profile")]
public class BlockParticleProfile : ScriptableObject
{
    [field: SerializeField] public Mesh[] Meshes { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField, Min(0f)] public float ForceMultiplier { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public int MinParticles { get; private set; } = 3;
    [field: SerializeField, Min(0f)] public int MaxParticles { get; private set; } = 50;
}
