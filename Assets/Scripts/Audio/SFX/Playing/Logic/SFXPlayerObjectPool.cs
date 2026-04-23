using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

public class SFXPlayerObjectPool : IDisposable
{
    private readonly ObjectPool<SFXPlayerView> _sfxPlayerPool;
    private readonly Dictionary<SFXPlayerView, IDisposable> _subscriptions = new();

    public SFXPlayerObjectPool(SFXPlayerView playerPrefab, AudioSettings audioSettings)
    {
        Transform playerPoolTransform = new GameObject("SFXPlayerPool").transform;

        _sfxPlayerPool = new ObjectPool<SFXPlayerView>(
            createFunc: () => Object.Instantiate(playerPrefab, playerPoolTransform),
            actionOnGet: playerView => playerView.gameObject.SetActive(true),
            actionOnRelease: OnRelease,
            defaultCapacity: audioSettings.PoolDefaultCapacity,
            maxSize: audioSettings.PoolMaxSize
            );
    }

    public void Dispose()
    {
        foreach (var subscription in _subscriptions.Values)
            subscription.Dispose();

        _subscriptions.Clear();
    }

    public void PlaySFX(Transform target, AudioResource audioResource)
    {
        SFXPlayerView playerView = _sfxPlayerPool.Get();
        playerView.Play(target, audioResource);

        IDisposable subscription = playerView.IsPlaying
            .Where(isPlaying => !isPlaying)
            .Subscribe(_ => _sfxPlayerPool.Release(playerView));

        _subscriptions[playerView] = subscription;
    }

    private void OnRelease(SFXPlayerView playerView)
    {
        _subscriptions[playerView].Dispose();
        _subscriptions.Remove(playerView);

        playerView.gameObject.SetActive(false);
    }
}
