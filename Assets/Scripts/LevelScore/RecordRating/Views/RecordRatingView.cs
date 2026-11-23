using AngryBirds3D.LevelScore.RatingEvaluation;
using Cysharp.Threading.Tasks;
using LitMotion;
using Ovoshnig.UI.Basic;
using R3;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace AngryBirds3D.LevelScore.RecordRating
{
    public class RecordRatingView : UIView
    {
        [SerializeField] private VisualElementReference<VisualElement> _ratingContainerReference = new();
        [SerializeField] private RatingEvaluatorView _evaluatorView;
        [SerializeField] private Sprite _activeStar;
        [SerializeField] private Sprite _unactiveStar;
        [SerializeField, Min(0f)] private float _duration = 1f;

        private VisualElement _ratingContainer;
        private VisualElement[] _stars;
        private MotionHandle _handle;

        protected override bool IsVisualReady => _stars != null;

        private void Awake()
        {
            _ratingContainerReference.RegisterReferenceResolvedCallback(OnContainerResolved);
            _ratingContainerReference.RegisterReferenceUnloadedCallback(OnContainerUnloaded);
        }

        private void Start()
        {
            _evaluatorView.Shown
                .Subscribe(_ => ShowAsync().Forget())
                .RegisterTo(destroyCancellationToken);
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
            base.OnDestroy();
        }

        public void SetStarCount(int count)
        {
            foreach (var star in _stars)
                star.style.backgroundImage = new StyleBackground(_unactiveStar);

            for (int i = 0; i < count && i < _stars.Length; i++)
                _stars[i].style.backgroundImage = new StyleBackground(_activeStar);
        }

        public async UniTask ShowAsync()
        {
            _handle.TryCancel();

            _handle = LMotion.Create(0f, 1f, _duration)
                .WithEase(Ease.OutExpo)
                .Bind(alpha => _ratingContainer.style.opacity = alpha);

            await _handle.ToUniTask(destroyCancellationToken);
        }

        private void OnContainerResolved(VisualElement container)
        {
            _ratingContainer = container;
            _ratingContainer.style.opacity = 0f;
            _stars = container.Children().ToArray();
            RefreshEnabledState();
        }

        private void OnContainerUnloaded(VisualElement container)
        {
            _handle.TryCancel();
            _ratingContainer = null;
            _stars = null;
            RefreshEnabledState();
        }
    }
}
