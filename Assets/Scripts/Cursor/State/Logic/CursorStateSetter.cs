using Ovoshnig.UI.Input;
using R3;
using System;
using UnityEngine;
using VContainer.Unity;
using UnityCursor = UnityEngine.Cursor;

namespace Ovoshnig.Cursor.State;

public sealed class CursorStateSetter(CursorStateModel model, CursorConfiguration configuration,
    UIInputProvider uiInputProvider) : IStartable, IDisposable
{
    private readonly CursorStateModel _model = model;
    private readonly CursorConfiguration _configuration = configuration;
    private readonly UIInputProvider _uiInputProvider = uiInputProvider;
    private readonly CompositeDisposable _disposables = [];

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
        if (_configuration.TryGetCursorData(state, out CursorData data))
            UnityCursor.SetCursor(data.Texture, data.Hotspot, CursorMode.Auto);
    }

    private void OnClickPressed(bool isPressed)
    {
        if (isPressed && _model.CurrentState.CurrentValue == CursorState.UIHover)
            _model.SetState(CursorState.UIClick);
        else if (!isPressed && _model.CurrentState.CurrentValue == CursorState.UIClick)
            _model.SetState(CursorState.UIHover);
    }
}
