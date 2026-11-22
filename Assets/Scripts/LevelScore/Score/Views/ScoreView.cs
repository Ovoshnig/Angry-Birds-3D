using LitMotion;
using Ovoshnig.UI.Basic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AngryBirds3D.LevelScore.Score
{
    public class ScoreView : UIView
    {
        [SerializeField] private VisualElementReference<Label> _scoreLabelReference = new();
        [SerializeField, Min(0f)] private float _updateDuration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private Label _scoreLabel;
        private MotionHandle _handle;
        private int _currentDisplayedScore;

        protected override bool IsVisualReady => _scoreLabel != null;

        private void Awake()
        {
            _scoreLabelReference.RegisterReferenceResolvedCallback(OnScoreLabelResolved);
            _scoreLabelReference.RegisterReferenceUnloadedCallback(OnScoreLabelUnloaded);
        }

        protected override void OnDisable()
        {
            _handle.TryCancel();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            _handle.TryCancel();
            _scoreLabelReference.UnregisterReferenceResolvedCallback(OnScoreLabelResolved);
            _scoreLabelReference.UnregisterReferenceUnloadedCallback(OnScoreLabelUnloaded);
            base.OnDestroy();
        }

        public void SetScoreInstant(int value)
        {
            _handle.TryCancel();
            ApplyScoreText(value);
        }

        public void SetScoreSmoothly(int targetValue)
        {
            _handle.TryCancel();

            if (_currentDisplayedScore == targetValue)
                return;

            _handle = LMotion.Create(_currentDisplayedScore, targetValue, _updateDuration)
                .WithEase(_ease)
                .Bind(UpdateScoreOnMotion)
                .AddTo(gameObject);
        }

        private void OnScoreLabelResolved(Label label)
        {
            _scoreLabel = label;
            ApplyScoreText(_currentDisplayedScore);
            RefreshEnabledState();
        }

        private void OnScoreLabelUnloaded(Label label)
        {
            _handle.TryCancel();
            _scoreLabel = null;
            RefreshEnabledState();
        }

        private void UpdateScoreOnMotion(int score)
        {
            if (_currentDisplayedScore == score)
                return;

            ApplyScoreText(score);
        }

        private void ApplyScoreText(int score)
        {
            _currentDisplayedScore = score;
            _scoreLabel.text = $"{score:00000}";
        }
    }
}
