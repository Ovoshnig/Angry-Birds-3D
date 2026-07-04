using R3;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class SFXPlayerObjectPool : IDisposable
{
    private readonly ObjectPool<SFXPlayerView> _sfxPlayerPool;
    private readonly SFXCounter _sfxCounter;
    private readonly AudioSettings _audioSettings;
    private readonly GameObject _poolRoot;

    public SFXPlayerObjectPool(SFXPlayerView playerPrefab, SFXCounter sfxCounter, AudioSettings audioSettings)
    {
        _sfxCounter = sfxCounter;
        _audioSettings = audioSettings;

        _poolRoot = new GameObject("SFXPlayerPool");

        _sfxPlayerPool = new ObjectPool<SFXPlayerView>(
            createFunc: () => Object.Instantiate(playerPrefab, _poolRoot.transform),
            actionOnGet: playerView => playerView.gameObject.SetActive(true),
            actionOnRelease: playerView => playerView.gameObject.SetActive(false),
            defaultCapacity: audioSettings.PoolDefaultCapacity,
            maxSize: audioSettings.PoolMaxSize
        );
    }

    public void Dispose()
    {
        _sfxPlayerPool.Dispose();
        Object.Destroy(_poolRoot);
    }

    public void PlaySFX(AudioResource audioResource) => Play(audioResource);

    public void PlaySFX(Transform target, AudioResource audioResource) => Play(audioResource, target);

    private void Play(AudioResource audioResource, Transform target = null)
    {
        if (audioResource == null || _sfxCounter.GetCount(audioResource) >= _audioSettings.MaxSameSfxPlaying)
            return;

        SFXPlayerView playerView = _sfxPlayerPool.Get();

        if (target == null)
            playerView.Play2D(audioResource);
        else
            playerView.Play3D(target, audioResource);

        _sfxCounter.Increment(audioResource);

        playerView.Stopped
            .Take(1)
            .Subscribe(_ =>
            {
                _sfxPlayerPool.Release(playerView);
                _sfxCounter.Decrement(audioResource);
            })
            .RegisterTo(playerView.destroyCancellationToken);
    }
}
