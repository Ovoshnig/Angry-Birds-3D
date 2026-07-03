using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class TrailParticlePlayer : IStartable, IDisposable
{
    private readonly BirdFlyer _birdFlyer;
    private readonly BirdPowerActivator _birdPowerActivator;
    private readonly SplitInto3BirdPower _splitInto3Power;
    private readonly GameObject _poolRoot;
    private readonly ObjectPool<TrailParticleView> _trailParticlePool;
    private readonly List<TrailParticleView> _currentParticles = new();
    private readonly List<TrailParticleView> _previousParticles = new();
    private readonly CompositeDisposable _disposables = new();

    public TrailParticlePlayer(BirdFlyer birdFlyer,
        BirdPowerActivator birdPowerActivator,
        SplitInto3BirdPower splitInto3Power,
        TrailParticleView particlePrefab,
        TrailParticleSettings settings)
    {
        _birdFlyer = birdFlyer;
        _birdPowerActivator = birdPowerActivator;
        _splitInto3Power = splitInto3Power;

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

    public void Start()
    {
        _birdFlyer.FlightStarted
            .Subscribe(birdEntityView => StartPlaying(birdEntityView.transform))
            .AddTo(_disposables);

        _birdFlyer.FlightInterrupted
            .Subscribe(birdEntityView => StopPlaying())
            .AddTo(_disposables);

        _birdPowerActivator.Activated
            .Where(entityView => entityView.PowerView.HasPowerParticle)
            .Subscribe(_ => PlayPowerParticle())
            .AddTo(_disposables);

        _splitInto3Power.CloneCreated
            .Subscribe(clone => StartPlaying(clone.transform))
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        foreach (var particle in _currentParticles)
            Object.Destroy(particle);

        _currentParticles.Clear();

        foreach (var particle in _previousParticles)
            Object.Destroy(particle);

        _previousParticles.Clear();

        _trailParticlePool.Dispose();
        Object.Destroy(_poolRoot);
    }

    private void StartPlaying(Transform birdTransform)
    {
        TrailParticleView particleView = _trailParticlePool.Get();
        particleView.Play(birdTransform);
        _currentParticles.Add(particleView);
    }

    private void StopPlaying()
    {
        foreach (TrailParticleView particle in _currentParticles)
            particle.StopEmitting();

        List<TrailParticleView> particlesToRelease = new(_previousParticles);
        _previousParticles.Clear();

        ReleaseParticlesAsync(particlesToRelease).Forget();

        _previousParticles.AddRange(_currentParticles);
        _currentParticles.Clear();
    }

    private void PlayPowerParticle()
    {
        if (_currentParticles.Count > 0)
            _currentParticles[0].EmitPowerParticle();
    }

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
