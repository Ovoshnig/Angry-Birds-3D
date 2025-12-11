using R3;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayerView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _isPlaying = new(false);

    private AudioSource _audioSource;

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Observable
            .EveryValueChanged(_audioSource, a => a.isPlaying)
            .Subscribe(isPlaying => _isPlaying.Value = isPlaying)
            .AddTo(this);
    }

    public void Play(Vector3 position, AudioResource audioResource)
    {
        transform.position = position;

        _audioSource.resource = audioResource;
        _audioSource.Play();
    }
}
