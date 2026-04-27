using R3;
using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayerView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _isPlaying = new(false);

    private AudioSource _audioSource;
    private IDisposable _followSubscription = null;

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Observable
            .EveryValueChanged(_audioSource, a => a.isPlaying)
            .Where(isPlaying => !isPlaying)
            .Subscribe(_ =>
            {
                _isPlaying.Value = false;
                StopFollowing();
            })
            .AddTo(this);
    }

    private void OnDestroy()
    {
        _followSubscription?.Dispose();
        _isPlaying.Dispose();
    }

    public void Play2D(AudioResource audioResource)
    {
        _audioSource.spatialBlend = 0f;
        _audioSource.resource = audioResource;
        _audioSource.Play();
        _isPlaying.Value = true;
    }

    public void Play3D(Transform target, AudioResource audioResource)
    {
        _audioSource.spatialBlend = 1f;
        _audioSource.resource = audioResource;
        _audioSource.Play();
        _isPlaying.Value = true;

        StartFollowing(target);
    }

    private void StartFollowing(Transform target)
    {
        StopFollowing();

        if (target == null)
            return;

        transform.position = target.position;

        _followSubscription = Observable.EveryUpdate(destroyCancellationToken)
            .Subscribe(_ =>
            {
                if (target != null)
                    transform.position = target.position;
                else
                    StopFollowing();
            });
    }

    private void StopFollowing()
    {
        _followSubscription?.Dispose();
        _followSubscription = null;
    }
}
