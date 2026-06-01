using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class RecordRatingView : UIView
{
    [SerializeField] private RatingEvaluatorView _evaluatorView;
    [SerializeField] private Sprite _activeStar;
    [SerializeField] private Sprite _unactiveStar;
    [SerializeField, Min(0f)] private float _duration = 1f;

    private CanvasGroup _canvasGroup;
    private Image[] _images;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _images = GetComponentsInChildren<Image>();
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;

        _evaluatorView.Shown
            .Subscribe(_ => ShowAsync().Forget())
            .RegisterTo(destroyCancellationToken);
    }

    public void SetStarCount(int count)
    {
        foreach (var image in _images)
            image.sprite = _unactiveStar;

        for (int i = 0; i < count; i++)
            _images[i].sprite = _activeStar;
    }

    public async UniTask ShowAsync()
    {
        await LMotion.Create(0f, 1f, _duration)
            .WithEase(Ease.OutExpo)
            .BindToAlpha(_canvasGroup)
            .ToUniTask(destroyCancellationToken);
    }
}
