using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SlingshotSFXPlayerView : MonoBehaviour
{
    [SerializeField] private AudioClip _draggingClip;
    [SerializeField] private AudioResource _shotRandomContainer;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayDragging()
    {
        _audioSource.clip = _draggingClip;
        _audioSource.Play();
    }

    public void PlayShot()
    {
        _audioSource.resource = _shotRandomContainer;
        _audioSource.Play();
    }
}
