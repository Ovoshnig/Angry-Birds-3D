using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PointsView : MonoBehaviour
{
    [SerializeField] private TextAnimationStrategy _animationStrategy;

    private TMP_Text _text;
    private Sequence _sequence;
    private Material _cachedMaterial;
    private Camera _camera;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
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

    public void Show(Vector3 position)
    {
        if (_animationStrategy == null)
        {
            Debug.LogWarning("Strategy not assigned!");
            return;
        }

        _sequence?.Kill();

        transform.parent.position = position;
        transform.rotation = _camera.transform.rotation;

        _animationStrategy.ResetState(_text, _cachedMaterial);
        _sequence = _animationStrategy.CreateSequence(_text, _cachedMaterial);
    }
}
