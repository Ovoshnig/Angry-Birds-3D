using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(ParticleSystemRenderer))]
public class BlockParticleView : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private ParticleSystemRenderer _particleSystemRenderer;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
    }

    public void Emit(Vector3 position, int count, BlockParticleProfile particleProfile)
    {
        _particleSystemRenderer.renderMode = ParticleSystemRenderMode.Mesh;
        _particleSystemRenderer.SetMeshes(particleProfile.Meshes);
        _particleSystemRenderer.material = particleProfile.Material;

        ParticleSystem.EmitParams emitParams = new() { position = position, applyShapeToPosition = true };
        _particleSystem.Emit(emitParams, count);
    }

    public void Emit(Vector3 position, float force, BlockParticleProfile particleProfile)
    {
        int count = Mathf.Clamp(Mathf.RoundToInt(force * particleProfile.ForceMultiplier),
            particleProfile.MinParticles,
            particleProfile.MaxParticles);

        Emit(position, count, particleProfile);
    }
}
