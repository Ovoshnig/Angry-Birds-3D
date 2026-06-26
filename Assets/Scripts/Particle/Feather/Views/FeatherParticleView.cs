using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FeatherParticleView : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 3f;
    [SerializeField] private int _maxParticles = 25;
    [SerializeField] private int _minParticles = 3;

    private ParticleSystem _particleSystem;

    private void Awake() => _particleSystem = GetComponent<ParticleSystem>();

    public void Emit(Vector3 position, Color color, float force)
    {
        int count = Mathf.Clamp(Mathf.RoundToInt(force * _forceMultiplier), _minParticles, _maxParticles);

        ParticleSystem.EmitParams emitParams = new()
        {
            position = position,
            startColor = color
        };

        _particleSystem.Emit(emitParams, count);
    }

    public void EmitMax(Vector3 position, Color color) => Emit(position, color, _maxParticles);
}
