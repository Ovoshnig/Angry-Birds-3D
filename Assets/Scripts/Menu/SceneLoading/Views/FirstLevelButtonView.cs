using R3;
using UnityEngine;

public class FirstLevelButtonView : SceneButtonView
{
    [SerializeField] private PlayButtonPrinterView _playButtonPrinterView;

    protected override void Awake()
    {
        base.Awake();

        SetInteractable(false);

        _playButtonPrinterView.Completed
            .Subscribe(_ => SetInteractable(true))
            .AddTo(this);
    }
}
