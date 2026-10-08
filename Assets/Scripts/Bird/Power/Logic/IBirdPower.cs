using AngryBirds3D.Bird.Entity;

namespace AngryBirds3D.Bird.Power
{
    public interface IBirdPower
    {
        BirdPowerType Type { get; }
        void Activate(BirdEntityView birdEntityView);
    }
}
