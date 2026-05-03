using R3;
using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayerView : MonoBehaviour
{
    private readonly Subject<Unit> _stopped = new();

    private AudioSource _audioSource;
    private IDisposable _followSubscription = null;
    private IDisposable _playbackSubscription = null;

    public Observable<Unit> Stopped => _stopped;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnDestroy()
    {
        _followSubscription?.Dispose();
        _playbackSubscription?.Dispose();
        _stopped.Dispose();
    }

    public void Play2D(AudioResource audioResource)
    {
        _followSubscription?.Dispose();

        _audioSource.spatialBlend = 0f;
        _audioSource.resource = audioResource;
        _audioSource.Play();

        StartTrackingPlayback();
    }

    public void Play3D(Transform target, AudioResource audioResource)
    {
        _audioSource.spatialBlend = 1f;
        _audioSource.resource = audioResource;
        _audioSource.Play();

        StartFollowing(target);
        StartTrackingPlayback();
    }

    private void StartTrackingPlayback()
    {
        _playbackSubscription?.Dispose();

        _playbackSubscription = Observable.EveryUpdate(destroyCancellationToken)
            .Where(_ => !_audioSource.isPlaying)
            .Take(1)
            .Subscribe(_ => _stopped.OnNext(Unit.Default));
    }

    private void StartFollowing(Transform target)
    {
        _followSubscription?.Dispose();

        _followSubscription = Observable.EveryUpdate(destroyCancellationToken)
            .TakeWhile(_ => target != null && _audioSource.isPlaying)
            .Subscribe(_ => transform.position = target.position);
    }
}
