using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TrailParticleView : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _disappearanceDuration = 0.5f;

    private ParticleSystem _particleSystem;
    private ParticleSystem.EmissionModule _emissionModule;
    private MotionHandle _scaleHandle;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _emissionModule = _particleSystem.emission;
    }

    private void Start() => _particleSystem.Stop();

    public void Play(Transform birdTransform)
    {
        _scaleHandle.TryCancel();

        transform.SetParent(birdTransform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        _particleSystem.Play();
        _emissionModule.enabled = true;
    }

    public void StopEmitting()
    {
        transform.SetParent(null);
        _emissionModule.enabled = false;
    }

    public async UniTask StopAsync()
    {
        _scaleHandle.TryCancel();

        _scaleHandle = LMotion.Create(Vector3.one, Vector3.zero, _disappearanceDuration)
            .WithEase(Ease.InSine)
            .BindToLocalScale(transform);

        await _scaleHandle.ToUniTask(destroyCancellationToken);

        _particleSystem.Stop();
        _particleSystem.Clear();
    }
}
