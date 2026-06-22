using System.Collections.Generic;
using System.Linq;

public class BirdPowerRegistry
{
    private readonly IReadOnlyDictionary<BirdPowerType, IBirdPower> _powers;

    public BirdPowerRegistry(IReadOnlyList<IBirdPower> powers) =>
        _powers = powers.ToDictionary(p => p.Type);

    public bool TryGet(BirdPowerType type, out IBirdPower power) =>
        _powers.TryGetValue(type, out power);
}
