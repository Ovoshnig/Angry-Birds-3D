using R3;
using System.Collections.Generic;

public class TextPrinterInputProviderMediator : Mediator
{
    private readonly IReadOnlyList<TextPrinterView> _textPrinterViews;
    private readonly MenuInputProvider _menuInputProvider;

    public TextPrinterInputProviderMediator(IReadOnlyList<TextPrinterView> textPrinterViews,
        MenuInputProvider menuInputProvider)
    {
        _textPrinterViews = textPrinterViews;
        _menuInputProvider = menuInputProvider;
    }

    public override void Start()
    {
        _menuInputProvider.SkipTextPrintingPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => OnSkipTextPrintingPressed())
            .AddTo(Disposables);
    }

    private void OnSkipTextPrintingPressed()
    {
        foreach (var printingView in _textPrinterViews)
            if (printingView.IsPrinting.CurrentValue)
                printingView.TryCompletePrinting();
    }
}
