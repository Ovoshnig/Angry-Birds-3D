using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TextAnimationStrategy _animationStrategy;

    private readonly Subject<Unit> _stopped = new();

    private TMP_Text _text;
    private Material _cachedMaterial;
    private Sequence _sequence = null;
    private Camera _camera;

    public Observable<Unit> Stopped => _stopped;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _cachedMaterial = _text.fontMaterial;

        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        if (_cachedMaterial != null)
            Destroy(_cachedMaterial);

        _stopped.Dispose();
    }

    public void Show(Vector3 position, DestructionPointsSettings pointsSettings)
    {
        if (_animationStrategy == null)
        {
            Debug.LogWarning("Animation strategy not assigned!");
            return;
        }

        if (_sequence != null && _sequence.IsActive())
        {
            _sequence.onKill = null;
            _sequence.Kill();
        }

        transform.SetPositionAndRotation(position, _camera.transform.rotation);

        _text.SetText("{0}", pointsSettings.Points);
        _text.color = pointsSettings.Color;
        _text.fontSize = pointsSettings.FontSize;

        _animationStrategy.ResetState(_text, _cachedMaterial);
        _sequence = _animationStrategy.CreateSequence(_text, _cachedMaterial);

        _sequence
            .SetLink(gameObject)
            .OnKill(() => _stopped.OnNext(Unit.Default));
    }
}
