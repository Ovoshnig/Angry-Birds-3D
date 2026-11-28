using Ovoshnig.UI.Basic;
using Ovoshnig.UI.Input;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.UI.TextPrinting;

public class InputProviderTextPrinterViewsMediator(UIInputProvider uiInputProvider,
    IReadOnlyList<TextPrinterView> views) : UIViewsMediator<TextPrinterView>(views)
{
    private readonly UIInputProvider _uiInputProvider = uiInputProvider;

    protected override void OnViewEnabled(TextPrinterView view, CompositeDisposable viewDisposables)
    {
        _uiInputProvider.SkipTextPrintingPressed
            .Where(isPressed => isPressed && view.IsPrinting.CurrentValue)
            .Subscribe(_ => view.TryCompletePrinting())
            .AddTo(viewDisposables);
    }
}
