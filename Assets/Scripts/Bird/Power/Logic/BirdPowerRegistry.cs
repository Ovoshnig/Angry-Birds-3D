using System.Collections.Generic;
using System.Linq;

namespace AngryBirds3D.Bird.Power;

public class BirdPowerRegistry(IReadOnlyList<IBirdPower> powers)
{
    private readonly IReadOnlyDictionary<BirdPowerType, IBirdPower> _powers = powers
        .ToDictionary(p => p.Type);

    public bool TryGet(BirdPowerType type, out IBirdPower power) =>
        _powers.TryGetValue(type, out power);
}
