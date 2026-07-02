public interface IBirdPower
{
    BirdPowerType Type { get; }
    void Activate(BirdEntityView birdEntityView);
}
