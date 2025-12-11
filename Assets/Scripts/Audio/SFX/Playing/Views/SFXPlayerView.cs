using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayerView : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void Play(Vector3 position, AudioResource audioResource)
    {
        transform.position = position;

        _audioSource.resource = audioResource;
        _audioSource.Play();
    }
}
