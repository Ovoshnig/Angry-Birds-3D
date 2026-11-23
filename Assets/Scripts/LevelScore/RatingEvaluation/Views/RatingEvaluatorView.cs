using Cysharp.Threading.Tasks;
using LitMotion;
using Ovoshnig.UI.Basic;
using R3;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AngryBirds3D.LevelScore.RatingEvaluation
{
    public class RatingEvaluatorView : UIView
    {
        [SerializeField] private VisualElementReference<VisualElement> _ratingContainerReference = new();
        [SerializeField] private Sprite _activeStar;
        [SerializeField] private Sprite _unactiveStar;
        [SerializeField] private Ease _jumpEase = Ease.OutQuint;
        [SerializeField] private Ease _fallEase = Ease.OutBounce;
        [SerializeField, Min(0f)] private float _motionDeltaY = 300f;
        [SerializeField, Min(0f)] private float _jumpDuration = 0.5f;
        [SerializeField, Min(0f)] private float _fallDuration = 1f;

        private readonly Subject<Unit> _shown = new();

        private VisualElement[] _stars;
        private MotionHandle _handle;

        public Observable<Unit> Shown => _shown;

        protected override bool IsVisualReady => _stars != null;

        private void Awake()
        {
            _ratingContainerReference.RegisterReferenceResolvedCallback(OnContainerResolved);
            _ratingContainerReference.RegisterReferenceUnloadedCallback(OnContainerUnloaded);
        }

        protected override void OnDisable()
        {
            _handle.TryCancel();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            _handle.TryCancel();
            _ratingContainerReference.UnregisterReferenceResolvedCallback(OnContainerResolved);
            _ratingContainerReference.UnregisterReferenceUnloadedCallback(OnContainerUnloaded);
            _shown.Dispose();
            base.OnDestroy();
        }

        public async UniTask ShowStarAsync(int count)
        {
            _handle.TryCancel();

            foreach (var star in _stars)
            {
                star.style.backgroundImage = new StyleBackground(_unactiveStar);
                star.style.translate = new Translate(0, 0);
            }

            for (int i = 0; i < count && i < _stars.Length; i++)
            {
                VisualElement star = _stars[i];

                _handle = LMotion.Create(0f, -_motionDeltaY, _jumpDuration)
                    .WithEase(_jumpEase)
                    .Bind(y => star.style.translate = new Translate(0, y));

                await _handle.ToUniTask(destroyCancellationToken);

                star.style.backgroundImage = new StyleBackground(_activeStar);

                _handle = LMotion.Create(-_motionDeltaY, 0f, _fallDuration)
                    .WithEase(_fallEase)
                    .Bind(y => star.style.translate = new Translate(0, y));

                await _handle.ToUniTask(destroyCancellationToken);
            }

            _shown.OnNext(Unit.Default);
        }

        private void OnContainerResolved(VisualElement container)
        {
            _stars = container.Children().ToArray();
            RefreshEnabledState();
        }

        private void OnContainerUnloaded(VisualElement container)
        {
            _handle.TryCancel();
            _stars = null;
            RefreshEnabledState();
        }
    }
}
