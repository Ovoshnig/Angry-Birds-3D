using System;

public class HealthModel
{
    private float _health;

    public HealthModel(float health) => _health = health;

    public float Health => _health;

    public void Decrement(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Damage cannot be negative.");

        if (amount == 0)
            return;

        _health -= amount;
    }
}
