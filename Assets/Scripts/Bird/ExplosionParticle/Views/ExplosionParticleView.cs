using UnityEngine;

public class ExplosionParticleView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireSmokeSystem;
    [SerializeField] private ParticleSystem _shockwaveSystem;

    public void Play(Vector3 position, float radius)
    {
        transform.position = position;
        _shockwaveSystem.transform.localScale = radius * Vector3.one;

        _fireSmokeSystem.Play();
        _shockwaveSystem.Play();
    }
}
