using R3;

public class GameRestarter
{
    private readonly Subject<Unit> _restarted = new();

    public Observable<Unit> Restarted => _restarted;

    public void Restart() => _restarted.OnNext(Unit.Default);
}
