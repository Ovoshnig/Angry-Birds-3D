using R3;
using UnityEngine;

public class GameQuitterMediator : Mediator
{
    private readonly GameQuitButtonView _gameQuitButtonView;

    public GameQuitterMediator(GameQuitButtonView gameQuitButtonView) =>
        _gameQuitButtonView = gameQuitButtonView;

    public override void Start()
    {
        _gameQuitButtonView.Clicked
            .Subscribe(_ => Application.Quit())
            .AddTo(Disposables);
    }
}
