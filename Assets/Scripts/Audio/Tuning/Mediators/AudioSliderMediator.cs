using R3;

public class AudioSliderMediator : Mediator
{
    private readonly MusicSliderModel _musicModel;
    private readonly SFXSliderModel _sfxModel;
    private readonly AudioSliderView[] _sliderViews;

    public AudioSliderMediator(MusicSliderModel musicModel, SFXSliderModel sfxModel,
        AudioSliderView[] sliderViews)
    {
        _musicModel = musicModel;
        _sfxModel = sfxModel;
        _sliderViews = sliderViews;
    }

    public override void Start()
    {
        foreach (var sliderView in _sliderViews)
        {
            if (sliderView is MusicSliderView)
                BindModelAndView(_musicModel, sliderView);
            else if (sliderView is SFXSliderView)
                BindModelAndView(_sfxModel, sliderView);
        }
    }

    private void BindModelAndView(AudioSliderModel sliderModel, AudioSliderView sliderView)
    {
        sliderView.SetMinValue(sliderModel.MinValue);
        sliderView.SetMaxValue(sliderModel.MaxValue);

        sliderModel.Value
            .Subscribe(value => sliderView.SetValueWithoutNotify(value))
            .AddTo(Disposables);

        sliderView.ValueChanged
            .Subscribe(value => sliderModel.SetClampedValue(value))
            .AddTo(Disposables);
    }
}
