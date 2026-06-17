using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class TrailParticlePlayer : IStartable, IDisposable
{
    private readonly TrailParticleView _particlePrefab;

    private TrailParticleView _particleFirst = null;
    private TrailParticleView _particleSecond = null;

    public TrailParticlePlayer(TrailParticleView particlePrefab) => _particlePrefab = particlePrefab;

    public void Start()
    {
        _particleFirst = Object.Instantiate(_particlePrefab);
        _particleFirst.name = $"{nameof(TrailParticleView)}1";

        _particleSecond = Object.Instantiate(_particlePrefab);
        _particleSecond.name = $"{nameof(TrailParticleView)}2";
    }

    public void Dispose()
    {
        if (_particleFirst != null)
            Object.Destroy(_particleFirst.gameObject);

        if (_particleSecond != null)
            Object.Destroy(_particleSecond.gameObject);
    }

    public void StartPlaying(Transform birdTransform)
    {
        (_particleFirst, _particleSecond) = (_particleSecond, _particleFirst);
        _particleFirst.Play(birdTransform);
    }

    public void StopPlaying()
    {
        _particleFirst.StopEmitting();
        _particleSecond.StopAsync().Forget();
    }
}
