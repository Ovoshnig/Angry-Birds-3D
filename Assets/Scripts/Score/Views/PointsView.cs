using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TextAnimationStrategy _animationStrategy;

    private readonly ReactiveProperty<bool> _isPlaying = new(false);

    private TMP_Text _text;
    private Sequence _sequence;
    private Material _cachedMaterial;
    private Camera _camera;

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _camera = Camera.main;

        _cachedMaterial = _text.fontMaterial;
    }

    private void Start()
    {
        if (_animationStrategy != null)
            _animationStrategy.ResetState(_text, _cachedMaterial);
    }

    private void OnDestroy()
    {
        _sequence?.Kill();

        if (_cachedMaterial != null)
            Destroy(_cachedMaterial);
    }

    public void Show(Vector3 position, int points)
    {
        if (_animationStrategy == null)
        {
            Debug.LogWarning("Strategy not assigned!");
            return;
        }

        if (_sequence != null)
        {
            _sequence.onComplete = null;
            _sequence.onKill = null;
            _sequence.Kill();
        }

        transform.SetPositionAndRotation(position, _camera.transform.rotation);
        _text.text = points.ToString();

        _animationStrategy.ResetState(_text, _cachedMaterial);
        _sequence = _animationStrategy.CreateSequence(_text, _cachedMaterial);

        _isPlaying.Value = true;

        _sequence
            .OnComplete(() => _isPlaying.Value = false)
            .OnKill(() => _isPlaying.Value = false);
    }
}
