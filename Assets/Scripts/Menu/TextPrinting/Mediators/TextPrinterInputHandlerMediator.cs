using R3;
using System;
using System.Linq;
using UnityEngine;

public class TextPrinterInputHandlerMediator : Mediator
{
    [SerializeField] private TextPrinterView[] _textPrinterViews;
    [SerializeField] private MenuInputHandler _menuInputHandler;

    public TextPrinterInputHandlerMediator(TextPrinterView[] textPrinterViews, 
        MenuInputHandler menuInputHandler)
    {
        _textPrinterViews = textPrinterViews;
        _menuInputHandler = menuInputHandler;
    }

    public override void Initialize()
    {
        _menuInputHandler.SkipTextPrintingPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => OnSkipTextPrintingPressed())
            .AddTo(CompositeDisposable);
    }

    private void OnSkipTextPrintingPressed()
    {
        foreach (var printingView in _textPrinterViews.Where(p => p.IsPrinting.CurrentValue))
            printingView.CancelPrinting();
    }
}
