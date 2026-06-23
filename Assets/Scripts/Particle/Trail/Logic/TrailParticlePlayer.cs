using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class TrailParticlePlayer : IDisposable
{
    private readonly GameObject _poolRoot;
    private readonly ObjectPool<TrailParticleView> _trailParticlePool;
    private readonly List<TrailParticleView> _currentParticles = new();
    private readonly List<TrailParticleView> _previousParticles = new();

    public TrailParticlePlayer(TrailParticleView particlePrefab, TrailParticleSettings settings)
    {
        _poolRoot = new GameObject("TrailParticlePlayerPool");

        _trailParticlePool = new ObjectPool<TrailParticleView>(
            createFunc: () => Object.Instantiate(particlePrefab, _poolRoot.transform),
            actionOnGet: particleView =>
            {
                particleView.transform.SetParent(null);
                particleView.gameObject.SetActive(true);
            },
            actionOnRelease: particleView =>
            {
                particleView.transform.SetParent(_poolRoot.transform);
                particleView.gameObject.SetActive(false);
            },
            defaultCapacity: settings.PoolDefaultCapacity,
            maxSize: settings.PoolMaxSize
        );
    }

    public void Dispose()
    {
        foreach (var particle in _currentParticles)
            Object.Destroy(particle);

        _currentParticles.Clear();

        foreach (var particle in _previousParticles)
            Object.Destroy(particle);

        _previousParticles.Clear();

        _trailParticlePool.Dispose();
        Object.Destroy(_poolRoot);
    }

    public void StartPlaying(Transform birdTransform)
    {
        TrailParticleView particleView = _trailParticlePool.Get();
        particleView.Play(birdTransform);
        _currentParticles.Add(particleView);
    }

    public void StopPlaying()
    {
        foreach (TrailParticleView particle in _currentParticles)
            particle.StopEmitting();

        List<TrailParticleView> particlesToRelease = new(_previousParticles);
        _previousParticles.Clear();

        ReleaseParticlesAsync(particlesToRelease).Forget();

        _previousParticles.AddRange(_currentParticles);
        _currentParticles.Clear();
    }

    public void PlayPowerParticle() => _currentParticles[0].EmitPowerParticle();

    private async UniTaskVoid ReleaseParticlesAsync(List<TrailParticleView> particles)
    {
        List<UniTask> tasks = new();

        foreach (TrailParticleView particle in particles)
            tasks.Add(particle.StopAsync());

        await UniTask.WhenAll(tasks);

        foreach (TrailParticleView particle in particles)
            _trailParticlePool.Release(particle);
    }
}
