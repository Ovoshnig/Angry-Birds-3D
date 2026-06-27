using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public sealed class CursorStateSetter : IStartable, IDisposable
{
    private readonly CursorStateModel _model;
    private readonly CursorConfiguration _configuration;
    private readonly UIInputProvider _uiInputProvider;
    private readonly CompositeDisposable _disposables = new();

    public CursorStateSetter(CursorStateModel model,
        CursorConfiguration configuration,
        UIInputProvider uiInputProvider)
    {
        _model = model;
        _configuration = configuration;
        _uiInputProvider = uiInputProvider;
    }

    public void Start()
    {
        _model.CurrentState
            .Subscribe(OnCursorStateChanged)
            .AddTo(_disposables);

        _uiInputProvider.ClickPressed
            .Subscribe(OnClickPressed)
            .AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();

    private void OnCursorStateChanged(CursorState state)
    {
        switch (state)
        {
            case CursorState.UIHover:
                Cursor.SetCursor(_configuration.UiHoverTexture, _configuration.UiHoverHotspot, CursorMode.Auto);
                break;
            case CursorState.UIClick:
                Cursor.SetCursor(_configuration.UiClickTexture, _configuration.UiClickHotspot, CursorMode.Auto);
                break;
            case CursorState.GameplayHover:
                Cursor.SetCursor(_configuration.GameplayHoverTexture, _configuration.GameplayHoverHotspot, CursorMode.Auto);
                break;
            case CursorState.GameplayGrab:
                Cursor.SetCursor(_configuration.GameplayGrabTexture, _configuration.GameplayGrabHotspot, CursorMode.Auto);
                break;
        }
    }

    private void OnClickPressed(bool isPressed)
    {
        if (isPressed && _model.CurrentState.CurrentValue == CursorState.UIHover)
            _model.SetState(CursorState.UIClick);
        else if (!isPressed && _model.CurrentState.CurrentValue == CursorState.UIClick)
            _model.SetState(CursorState.UIHover);
    }
}
