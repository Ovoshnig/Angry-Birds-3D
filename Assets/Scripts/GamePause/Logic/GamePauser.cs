using R3;

public class GamePauser
{
    private readonly ReactiveProperty<bool> _isPause = new(false);

    public ReadOnlyReactiveProperty<bool> IsPause => _isPause;

    public void Pause() => SetPause(true);

    public void UnPause() => SetPause(false);

    private void SetPause(bool value) => _isPause.Value = value;
}
