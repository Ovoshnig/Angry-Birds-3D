using R3;
using UnityEngine;

public class PlayButtonPrinterView : TextPrinterView
{
    [SerializeField] private ScoreTablePrinterView _scoreTablePrinterView;

    protected override void Start()
    {
        TmpText.maxVisibleCharacters = 0;
        string initialText = TmpText.text;

        _scoreTablePrinterView.Completed
            .Subscribe(_ => Print(initialText))
            .RegisterTo(destroyCancellationToken);
    }
}
