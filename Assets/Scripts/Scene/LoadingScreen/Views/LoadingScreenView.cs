using Ovoshnig.UI.Basic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ovoshnig.Scene.LoadingScreen
{
    public class LoadingScreenView : UIView
    {
        [SerializeField] private VisualElementReference<ProgressBar> _progressBarReference;

        private const float PercentMultiplier = 100f;

        private ProgressBar _progressBar = null;

        protected override bool IsVisualReady => _progressBar != null;

        protected virtual void Awake()
        {
            _progressBarReference.RegisterReferenceResolvedCallback(OnProgressBarResolved);
            _progressBarReference.RegisterReferenceUnloadedCallback(OnProgressBarUnloaded);
        }

        protected override void OnDestroy()
        {
            _progressBarReference.UnregisterReferenceResolvedCallback(OnProgressBarResolved);
            _progressBarReference.UnregisterReferenceUnloadedCallback(OnProgressBarUnloaded);
            base.OnDestroy();
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        public void SetProgress(float progress) => _progressBar.value = PercentMultiplier * progress;

        private void OnProgressBarResolved(ProgressBar bar)
        {
            _progressBar = bar;
            RefreshEnabledState();
        }

        private void OnProgressBarUnloaded(ProgressBar bar)
        {
            _progressBar = null;
            RefreshEnabledState();
        }
    }
}
