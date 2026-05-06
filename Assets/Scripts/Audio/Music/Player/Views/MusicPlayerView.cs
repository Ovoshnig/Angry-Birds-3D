using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayerView : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void Play(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.clip = null;
        _audioSource.Stop();
    }
}
