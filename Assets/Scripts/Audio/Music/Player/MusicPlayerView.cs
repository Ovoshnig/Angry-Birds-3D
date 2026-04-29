using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayerView : MonoBehaviour
{
    private AudioSource _audioSource;

    public bool IsPlaying => _audioSource.isPlaying;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void SetClip(AudioClip clip) => _audioSource.clip = clip;

    public void Play() => _audioSource.Play();

    public void Stop() => _audioSource.Stop();
}
