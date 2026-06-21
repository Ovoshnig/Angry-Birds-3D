public class NoneBirdPower : IBirdPower
{
    public BirdPowerType Type => BirdPowerType.None;

    public void Activate(BirdEntityView birdEntityView, BirdPowerSettings powerSettings)
    {
    }
}
