using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PigSFXPlayerView : MonoBehaviour
{
    [SerializeField] private AudioResource _collisionRandomContainer;
    [SerializeField] private AudioResource _damageRandomContainer;
    [SerializeField] private AudioResource _destructionRandomContainer;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayCollision()
    {
        if (_audioSource.isPlaying)
            return;

        _audioSource.resource = _collisionRandomContainer;
        _audioSource.Play();
    }

    public void PlayDamage()
    {
        if (_audioSource.isPlaying)
            return;

        _audioSource.resource = _damageRandomContainer;
        _audioSource.Play();
    }

    public void PlayDestruction()
    {
        _audioSource.resource = _destructionRandomContainer;
        _audioSource.Play();
    }
}
