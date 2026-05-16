using R3;
using System.Collections.Generic;
using System.Linq;

public class AudioSliderMediator : UIListMediator<AudioSliderView>
{
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels;

    public AudioSliderMediator(IReadOnlyList<AudioSliderModel> sliderModels, IReadOnlyList<AudioSliderView> views)
        : base(views) => _sliderModels = sliderModels;

    protected override void OnViewEnabled(AudioSliderView view, CompositeDisposable viewDisposables)
    {
        AudioSliderModel model = _sliderModels.FirstOrDefault(m => m.Channel == view.Channel);

        if (model == null)
            return;

        view.SetMinValue(model.MinValue);
        view.SetMaxValue(model.MaxValue);

        model.Value
            .Subscribe(view.SetValueWithoutNotify)
            .AddTo(viewDisposables);

        view.ValueChanged
            .Subscribe(model.SetClampedValue)
            .AddTo(viewDisposables);
    }
}
