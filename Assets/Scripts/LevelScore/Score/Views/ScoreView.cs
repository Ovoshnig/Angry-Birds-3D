using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _updateDuration = 0.5f;

    private TMP_Text _text = null;
    private Tween _scoreTween = null;
    private int _currentDisplayedScore = 0;

    private void Awake() => _text = GetComponent<TMP_Text>();

    private void Start() => UpdateText(_currentDisplayedScore);

    public void SetScoreInstant(int value)
    {
        _scoreTween?.Kill();
        _currentDisplayedScore = value;
        UpdateText(value);
    }

    public void SetScoreSmoothly(int targetValue)
    {
        _scoreTween?.Kill();

        _scoreTween = DOVirtual.Int(_currentDisplayedScore, targetValue, _updateDuration, OnUpdateScore)
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject);
    }

    private void OnUpdateScore(int value)
    {
        _currentDisplayedScore = value;
        UpdateText(value);
    }

    private void UpdateText(int value) => _text.SetText("Score: {0:0000}", value);
}
