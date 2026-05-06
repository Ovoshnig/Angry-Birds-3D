using R3;
using System.Linq;
using UnityEngine;

public class TextPrinterInputProviderMediator : Mediator
{
    [SerializeField] private TextPrinterView[] _textPrinterViews;
    [SerializeField] private MenuInputProvider _menuInputProvider;

    public TextPrinterInputProviderMediator(TextPrinterView[] textPrinterViews,
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
        foreach (var printingView in _textPrinterViews.Where(p => p.IsPrinting.CurrentValue))
            printingView.CancelPrinting();
    }
}
