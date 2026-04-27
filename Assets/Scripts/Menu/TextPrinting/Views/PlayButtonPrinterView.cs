using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class PlayButtonPrinterView : TextPrinterView
{
    [SerializeField] private ScoreTablePrinterView _scoreTablePrinterView;

    protected override void Awake()
    {
        base.Awake();

        string initialText = TmpText.text;
        TmpText.text = string.Empty;

        _scoreTablePrinterView.Completed
            .Subscribe(_ => PrintAsync(initialText).Forget())
            .AddTo(this);
    }
}
