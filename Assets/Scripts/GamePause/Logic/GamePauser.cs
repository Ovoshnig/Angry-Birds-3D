using R3;
using System;

public class GamePauser : IDisposable
{
    private readonly ReactiveProperty<bool> _isPause = new(false);

    public ReadOnlyReactiveProperty<bool> IsPause => _isPause;

    public void Dispose() => _isPause.Dispose();

    public void Pause() => SetPause(true);

    public void UnPause() => SetPause(false);

    private void SetPause(bool value) => _isPause.Value = value;
}
