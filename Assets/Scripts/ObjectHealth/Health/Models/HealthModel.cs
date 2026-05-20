using System;
using UnityEngine;

public class HealthModel
{
    private float _health;

    public HealthModel(float health) => _health = health;

    public float Health => _health;

    public void ApplyDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "The amount of damage cannot be negative.");

        if (amount == 0)
            return;

        _health = Mathf.Max(0f, _health - amount);
    }
}
