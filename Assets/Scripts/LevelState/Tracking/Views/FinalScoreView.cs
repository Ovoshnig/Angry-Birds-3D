using Ovoshnig.UI.Basic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AngryBirds3D.LevelState.Tracking
{
    public class FinalScoreView : UIView
    {
        [SerializeField] private VisualElementReference<Label> _labelReference = new();

        private Label _label;

        protected override bool IsVisualReady => _label != null;

        private void Awake()
        {
            _labelReference.RegisterReferenceResolvedCallback(OnLabelResolved);
            _labelReference.RegisterReferenceUnloadedCallback(OnLabelUnloaded);
        }

        protected override void OnDestroy()
        {
            _labelReference.UnregisterReferenceResolvedCallback(OnLabelResolved);
            _labelReference.UnregisterReferenceUnloadedCallback(OnLabelUnloaded);
            base.OnDestroy();
        }

        public void SetScore(int value) => _label.text = $"SCORE: {value:0000}";

        private void OnLabelResolved(Label label)
        {
            _label = label;
            RefreshEnabledState();
        }

        private void OnLabelUnloaded(Label label)
        {
            _label = null;
            RefreshEnabledState();
        }
    }
}
