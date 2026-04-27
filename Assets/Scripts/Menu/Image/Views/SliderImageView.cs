using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SliderImageView : MonoBehaviour
{
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private Sprite[] _fillingSprites;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _sliderView.Value
            .Subscribe(SetFillingSprite)
            .AddTo(this);
    }

    private void SetFillingSprite(float value)
    {
        if (_image != null && _fillingSprites.Length > 1)
        {
            float percentagePerImage = 1f / (_fillingSprites.Length - 1);
            float fillingPercentage = (value - _sliderView.MinValue) / (_sliderView.MaxValue - _sliderView.MinValue);
            int spriteIndex = Mathf.CeilToInt(fillingPercentage / percentagePerImage);
            _image.sprite = _fillingSprites[spriteIndex];
        }
    }
}
