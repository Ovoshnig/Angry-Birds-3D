using R3;
using System.Collections.Generic;

public class TextPrinterInputProviderMediator : UIListMediator<TextPrinterView>
{
    private readonly UIInputProvider _uiInputProvider;

    public TextPrinterInputProviderMediator(UIInputProvider uiInputProvider, IReadOnlyList<TextPrinterView> views)
        : base(views) => _uiInputProvider = uiInputProvider;

    protected override void OnViewEnabled(TextPrinterView view, CompositeDisposable viewDisposables)
    {
        _uiInputProvider.SkipTextPrintingPressed
            .Where(isPressed => isPressed && view.IsPrinting.CurrentValue)
            .Subscribe(_ => view.TryCompletePrinting())
            .AddTo(viewDisposables);
    }
}
