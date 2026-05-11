using R3;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class SFXPlayerObjectPool
{
    private readonly ObjectPool<SFXPlayerView> _sfxPlayerPool;

    public SFXPlayerObjectPool(SFXPlayerView playerPrefab, AudioSettings audioSettings)
    {
        Transform playerPoolTransform = new GameObject("SFXPlayerPool").transform;

        _sfxPlayerPool = new ObjectPool<SFXPlayerView>(
            createFunc: () => Object.Instantiate(playerPrefab, playerPoolTransform),
            actionOnGet: playerView => playerView.gameObject.SetActive(true),
            actionOnRelease: playerView => playerView.gameObject.SetActive(false),
            defaultCapacity: audioSettings.PoolDefaultCapacity,
            maxSize: audioSettings.PoolMaxSize
        );
    }

    public void PlaySFX(AudioResource audioResource)
    {
        SFXPlayerView playerView = _sfxPlayerPool.Get();
        playerView.Play2D(audioResource);
        SubscribeToRelease(playerView);
    }

    public void PlaySFX(Transform target, AudioResource audioResource)
    {
        SFXPlayerView playerView = _sfxPlayerPool.Get();
        playerView.Play3D(target, audioResource);
        SubscribeToRelease(playerView);
    }

    private void SubscribeToRelease(SFXPlayerView playerView)
    {
        playerView.Stopped
            .Take(1)
            .Subscribe(_ => _sfxPlayerPool.Release(playerView))
            .RegisterTo(playerView.destroyCancellationToken);
    }
}
