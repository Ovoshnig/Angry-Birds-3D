using System;
using UnityEngine;

[Serializable]
public class BirdPowerSettings
{
    [field: SerializeField] public SplitInto3PowerSettings SplitInto3PowerSettings { get; private set; }
    [field: SerializeField] public BoostPowerSettings BoostPowerSettings { get; private set; }
    [field: SerializeField] public ExplosionPowerSettings ExplosionPowerSettings { get; private set; }
}
