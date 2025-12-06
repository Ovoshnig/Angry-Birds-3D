using System;

public class PigHealthModel
{
    private float _health;

    public PigHealthModel(float health) => _health = health;

    public float Health => _health;

    public void Decrement(float value)
    {
        if (_health - value < 0)
            throw new ArgumentOutOfRangeException(
                nameof(value), 
                "Decrement value cannot reduce health below zero");

        _health -= value;
    }
}
