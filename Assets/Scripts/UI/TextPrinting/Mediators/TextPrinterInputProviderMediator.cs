using R3;
using System.Collections.Generic;

public class TextPrinterInputProviderMediator : UIListMediator<TextPrinterView>
{
    private readonly UIInputProvider _uiInputProvider;

    public TextPrinterInputProviderMediator(IReadOnlyList<TextPrinterView> textPrinterViews,
        UIInputProvider uiInputProvider) : base(textPrinterViews) =>
        _uiInputProvider = uiInputProvider;

    protected override void OnViewEnabled(TextPrinterView printerView, CompositeDisposable disposables)
    {
        _uiInputProvider.SkipTextPrintingPressed
            .Where(isPressed => isPressed && printerView.IsPrinting.CurrentValue)
            .Subscribe(_ => printerView.TryCompletePrinting())
            .AddTo(disposables);
    }
}
