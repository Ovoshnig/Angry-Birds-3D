using R3;
using UnityEngine;

namespace Ovoshnig.UI.Basic
{
    public abstract class UIView : MonoBehaviour
    {
        private readonly ReactiveProperty<bool> _isEnabled = new(false);
        private bool _isComponentEnabled;

        public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

        protected virtual bool IsVisualReady => true;

        protected virtual void OnEnable()
        {
            _isComponentEnabled = true;
            RefreshEnabledState();
        }

        protected virtual void OnDisable()
        {
            _isComponentEnabled = false;
            RefreshEnabledState();
        }

        protected virtual void OnDestroy() => _isEnabled.Dispose();

        protected void RefreshEnabledState() =>
            _isEnabled.Value = _isComponentEnabled && IsVisualReady;
    }
}
