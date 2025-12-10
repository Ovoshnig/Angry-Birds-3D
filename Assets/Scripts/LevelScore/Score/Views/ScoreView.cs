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

    public TMP_Text Text
    {
        get
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();

            return _text;
        }
    }

    private void Start() => UpdateText(_currentDisplayedScore);

    private void OnDestroy() => _scoreTween?.Kill();

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
            .SetEase(Ease.OutQuad);
    }

    private void OnUpdateScore(int value)
    {
        _currentDisplayedScore = value;
        UpdateText(value);
    }

    private void UpdateText(int value) => Text.text = $"Score: {value:D4}";
}
