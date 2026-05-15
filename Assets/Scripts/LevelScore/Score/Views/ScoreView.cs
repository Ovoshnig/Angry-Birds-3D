using LitMotion;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : UIView
{
    [SerializeField, Min(0f)] private float _updateDuration = 0.5f;
    [SerializeField] private Ease _ease = Ease.OutQuad;

    private TMP_Text _text;
    private MotionHandle _handle;
    private int _currentDisplayedScore = 0;

    private void Awake() => _text = GetComponent<TMP_Text>();

    private void Start() => UpdateText(0);

    public void SetScoreInstant(int value)
    {
        _handle.TryCancel();
        UpdateText(value);
    }

    public void SetScoreSmoothly(int targetValue)
    {
        _handle.TryCancel();

        _handle = LMotion.Create(_currentDisplayedScore, targetValue, _updateDuration)
            .WithEase(_ease)
            .Bind(UpdateText)
            .AddTo(gameObject);
    }

    private void UpdateText(int score)
    {
        _text.SetText("Score: {0:00000}", score);
        _currentDisplayedScore = score;
    }
}
