using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SlingshotSFXPlayerView : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayDragging() => _audioSource.Play();
}
