using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BirdSFXPlayerView : MonoBehaviour
{
    [SerializeField] private AudioClip _flyingClip;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayFlying()
    {
        _audioSource.clip = _flyingClip;
        _audioSource.Play();
    }
}
