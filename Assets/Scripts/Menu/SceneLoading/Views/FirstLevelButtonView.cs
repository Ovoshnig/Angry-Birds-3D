using R3;
using UnityEngine;

public class FirstLevelButtonView : SceneButtonView
{
    [SerializeField] private PlayButtonPrinterView _playButtonPrinterView;

    private readonly CompositeDisposable _compositeDisposable = new();

    protected override void Awake()
    {
        base.Awake();
        SetInteractable(false);

        _playButtonPrinterView.Completed
            .Subscribe(_ => SetInteractable(true))
            .AddTo(_compositeDisposable);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _compositeDisposable.Dispose();
    }
}
