using Ovoshnig.Extensions.UIToolkit;
using R3;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ovoshnig.UI.Basic
{
    public abstract class PanelButtonView : UIView
    {
        [SerializeField] private VisualElementReference<Button> _buttonReference = new();

        private readonly Subject<Unit> _clicked = new();

        private Button _button;
        private IDisposable _clickDisposable = null;

        public Observable<Unit> Clicked => _clicked;

        protected override bool IsVisualReady => _button != null;

        private void Awake()
        {
            _buttonReference.RegisterReferenceResolvedCallback(OnButtonResolved);
            _buttonReference.RegisterReferenceUnloadedCallback(OnButtonUnloaded);
        }

        protected override void OnDestroy()
        {
            _clickDisposable?.Dispose();
            _clicked.Dispose();
            _buttonReference.UnregisterReferenceResolvedCallback(OnButtonResolved);
            _buttonReference.UnregisterReferenceUnloadedCallback(OnButtonUnloaded);
            base.OnDestroy();
        }

        public void SetInteractable(bool value) => _button.SetEnabled(value);

        private void OnButtonResolved(Button button)
        {
            _clickDisposable?.Dispose();
            _button = button;

            _clickDisposable = button.ClickedAsObservable()
                .Subscribe(_clicked.OnNext);

            RefreshEnabledState();
        }

        private void OnButtonUnloaded(Button button)
        {
            _clickDisposable?.Dispose();
            _clickDisposable = null;
            _button = null;
            RefreshEnabledState();
        }
    }
}
