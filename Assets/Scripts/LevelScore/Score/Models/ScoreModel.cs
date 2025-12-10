using R3;
using System;

public class ScoreModel
{
    private readonly ReactiveProperty<int> _score = new(0);

    public ReadOnlyReactiveProperty<int> Score => _score;

    public void Increase(int value)
    {
        if (value < 0)
            throw new ArgumentException("Value cannot be less than 0", nameof(value));

        _score.Value += value;
    }

    public void Reset() => _score.Value = 0;
}
