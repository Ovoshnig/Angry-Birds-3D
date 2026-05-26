using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class RatingEvaluatorView : UIView
{
    [SerializeField] private Sprite _activeStar;
    [SerializeField] private Sprite _unactiveStar;
    [SerializeField] private Ease _jumpEase = Ease.OutQuint;
    [SerializeField] private Ease _fallEase = Ease.OutBounce;
    [SerializeField, Min(0f)] private float _motionDeltaY = 300f;
    [SerializeField, Min(0f)] private float _jumpDuration = 0.5f;
    [SerializeField, Min(0f)] private float _fallDuration = 1f;

    private Image[] _images;

    [field: SerializeField, Min(0)] public int MaxScoreThreshold { get; private set; } = 15000;

    public int MaxStarCount { get; private set; }

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>();
        MaxStarCount = _images.Length;
    }

    public async UniTask SetStarCount(int count)
    {
        foreach (var image in _images)
            image.sprite = _unactiveStar;

        for (int i = 0; i < count; i++)
        {
            Image image = _images[i];

            Vector3 position = image.rectTransform.anchoredPosition;
            float yPosition = position.y;

            await LMotion.Create(yPosition, yPosition + _motionDeltaY, _jumpDuration)
                .WithEase(_jumpEase)
                .BindToAnchoredPositionY(image.rectTransform)
                .ToUniTask(destroyCancellationToken);

            image.sprite = _activeStar;

            await LMotion.Create(yPosition + _motionDeltaY, yPosition, _fallDuration)
                .WithEase(_fallEase)
                .BindToAnchoredPositionY(image.rectTransform)
                .ToUniTask(destroyCancellationToken);
        }
    }
}
