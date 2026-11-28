using System;
using UnityEngine;

namespace Ovoshnig.ObjectHealth.Health;

public class HealthModel(float health)
{
    public float Health { get; private set; } = health;

    public void ApplyDamage(float amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),
                "The amount of damage cannot be negative.");
        }

        if (amount == 0)
            return;

        Health = Mathf.Max(0f, Health - amount);
    }
}
