using R3;
using System.Collections.Generic;

public class TextPrinterInputProviderMediator : UIListMediator<TextPrinterView>
{
    private readonly MenuInputProvider _menuInputProvider;

    public TextPrinterInputProviderMediator(IReadOnlyList<TextPrinterView> textPrinterViews,
        MenuInputProvider menuInputProvider) : base(textPrinterViews) =>
        _menuInputProvider = menuInputProvider;

    protected override void OnViewEnabled(TextPrinterView printerView, CompositeDisposable disposables)
    {
        _menuInputProvider.SkipTextPrintingPressed
            .Where(isPressed => isPressed && printerView.IsPrinting.CurrentValue)
            .Subscribe(_ => printerView.TryCompletePrinting())
            .AddTo(disposables);
    }
}
