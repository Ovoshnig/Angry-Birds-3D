using R3;
using System.Collections.Generic;
using System.Linq;

public class AudioSliderMediator : UIListMediator<AudioSliderView>
{
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels;

    public AudioSliderMediator(IReadOnlyList<AudioSliderModel> sliderModels,
        IReadOnlyList<AudioSliderView> sliderViews) : base(sliderViews) =>
        _sliderModels = sliderModels;

    protected override void OnViewEnabled(AudioSliderView view, CompositeDisposable disposables)
    {
        AudioSliderModel model = _sliderModels.FirstOrDefault(m => m.Channel == view.Channel);

        if (model != null)
            BindModelAndView(model, view, disposables);
    }

    private void BindModelAndView(AudioSliderModel sliderModel, AudioSliderView sliderView,
        CompositeDisposable disposables)
    {
        sliderView.SetMinValue(sliderModel.MinValue);
        sliderView.SetMaxValue(sliderModel.MaxValue);

        sliderModel.Value
            .Subscribe(value => sliderView.SetValueWithoutNotify(value))
            .AddTo(disposables);

        sliderView.ValueChanged
            .Subscribe(value => sliderModel.SetClampedValue(value))
            .AddTo(disposables);
    }
}
