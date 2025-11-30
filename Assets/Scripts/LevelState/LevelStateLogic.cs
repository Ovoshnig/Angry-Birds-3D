using R3;

public enum LevelState { Idle, RubberDragging, BirdFlying }

public class LevelStateLogic
{
    private readonly ReactiveProperty<LevelState> _state = new(LevelState.Idle);

    public ReadOnlyReactiveProperty<LevelState> State => _state;

    public void SetState(LevelState state) => _state.OnNext(state);
}
