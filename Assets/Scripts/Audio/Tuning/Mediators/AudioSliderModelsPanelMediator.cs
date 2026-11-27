using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using System.Linq;

namespace Ovoshnig.Audio.Tuning;

public class AudioSliderModelsPanelMediator : UIViewMediator<AudioSliderPanelView>
{
    private readonly IReadOnlyList<AudioSliderModel> _sliderModels;

    public AudioSliderModelsPanelMediator(IReadOnlyList<AudioSliderModel> sliderModels,
        AudioSliderPanelView view) : base(view) => _sliderModels = sliderModels;

    protected override void OnViewEnabled(AudioSliderPanelView view, CompositeDisposable viewDisposables)
    {
        foreach (AudioSliderModel model in _sliderModels)
        {
            view.SetRange(model.Channel, model.MinValue, model.MaxValue);

            model.Value
                .Subscribe(value => view.SetValueWithoutNotify(model.Channel, value))
                .AddTo(viewDisposables);
        }

        view.ValueChanged
            .Subscribe(change =>
            {
                AudioSliderModel model = _sliderModels.FirstOrDefault(m => m.Channel == change.Channel);
                model?.SetClampedValue(change.Value);
            })
            .AddTo(viewDisposables);
    }
}
