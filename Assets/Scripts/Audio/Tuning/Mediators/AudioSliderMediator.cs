using R3;
using System.Collections.Generic;
using System.Linq;

public class AudioSliderMediator : Mediator
{
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels;
    private readonly IReadOnlyList<AudioSliderView> _sliderViews;

    public AudioSliderMediator(IReadOnlyList<AudioSliderModel> sliderModels,
        IReadOnlyList<AudioSliderView> sliderViews)
    {
        _sliderModels = sliderModels;
        _sliderViews = sliderViews;
    }

    public override void Start()
    {
        foreach (var view in _sliderViews)
        {
            AudioSliderModel model = _sliderModels.FirstOrDefault(m => m.Channel == view.Channel);

            if (model != null)
                BindModelAndView(model, view);
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
