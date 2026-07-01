using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class TrailParticleView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField, Min(0f)] private float _disappearanceDuration = 0.5f;

    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _mainModule;
    private ParticleSystem.EmissionModule _emissionModule;
    private MotionHandle _scaleHandle;
    private ParticleSystem.MinMaxCurve _startSize;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        _mainModule = _particleSystem.main;
        _startSize = _mainModule.startSize;

        _emissionModule = _particleSystem.emission;
    }

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
        transform.localScale = Vector3.one;

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

    public void EmitPowerParticle()
    {
        _mainModule.startSize = _gameSettings.TrailParticleSettings.PowerParticleSize;
        _particleSystem.Emit(1);
        _mainModule.startSize = _startSize;
    }
}
