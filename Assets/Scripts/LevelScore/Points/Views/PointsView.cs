using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using Ovoshnig.UI.Basic;
using R3;
using UnityEngine;
using UnityEngine.UIElements;

namespace AngryBirds3D.LevelScore.Points
{
    public class PointsView : UIView
    {
        [SerializeField] private VisualElementReference<Label> _labelReference;

        private readonly Subject<Unit> _completed = new();

        private Label _label;
        private Camera _camera;
        private MotionHandle _handle;

        public Observable<Unit> Completed => _completed;

        protected override bool IsVisualReady => _label != null;

        protected virtual void Awake()
        {
            _handle.TryCancel();
            _labelReference.RegisterReferenceResolvedCallback(OnLabelResolved);
            _labelReference.RegisterReferenceUnloadedCallback(OnLabelUnloaded);
            _camera = Camera.main;
        }

        protected override void OnDestroy()
        {
            _handle.TryCancel();
            _labelReference.UnregisterReferenceResolvedCallback(OnLabelResolved);
            _labelReference.UnregisterReferenceUnloadedCallback(OnLabelUnloaded);
            _completed.Dispose();
            base.OnDestroy();
        }

        public async UniTask ShowAsync(Vector3 position, PointsSettings pointsSettings)
        {
            _handle.TryCancel();

            if (_label == null || _label.panel == null)
                await UniTask.WaitUntil(() => _label != null && _label.panel != null,
                    cancellationToken: destroyCancellationToken);

            transform.SetPositionAndRotation(position, _camera.transform.rotation);

            _label.SetText(pointsSettings.Points);
            _label.style.color = pointsSettings.Color;
            _label.style.fontSize = pointsSettings.FontSize;

            _handle = LMotion.Create(0f, 1f, pointsSettings.AppearanceDuration)
                .WithEase(pointsSettings.AppearanceEase)
                .BindToLocalScaleXYZ(transform);

            await _handle.ToUniTask(destroyCancellationToken);
            await UniTask.WaitForSeconds(pointsSettings.ShowingDuration, cancellationToken: destroyCancellationToken);

            _handle = LMotion.Create(1f, 0f, pointsSettings.DisappearanceDuration)
                .WithEase(pointsSettings.DisappearanceEase)
                .BindToLocalScaleXYZ(transform);

            await _handle.ToUniTask(destroyCancellationToken);

            _completed.OnNext(Unit.Default);
        }

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
