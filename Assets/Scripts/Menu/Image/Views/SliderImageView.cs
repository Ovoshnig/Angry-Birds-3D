using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SliderImageView : UIView
{
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private Sprite[] _fillingSprites;

    private Image _image;

    private void Awake() => _image = GetComponent<Image>();

    private void Start()
    {
        _sliderView.ValueChanged
            .Subscribe(SetFillingSprite)
            .RegisterTo(destroyCancellationToken);
    }

    private void SetFillingSprite(float value)
    {
        if (_fillingSprites.Length <= 1)
            return;

        float percentagePerImage = 1f / (_fillingSprites.Length - 1);
        float fillingPercentage = (value - _sliderView.MinValue) / (_sliderView.MaxValue - _sliderView.MinValue);
        int spriteIndex = Mathf.CeilToInt(fillingPercentage / percentagePerImage);
        _image.sprite = _fillingSprites[spriteIndex];
    }
}
