using System;

public class HealthModel
{
    private float _health;

    public HealthModel(float health) => _health = health;

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
