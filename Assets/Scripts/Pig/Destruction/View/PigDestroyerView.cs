public class PigDestroyerView : ObjectDestroyerView
{
    protected override float MaxHealth => GameSettings.PigSettings.Health;

    public override void Damage(float _)
    {
    }
}
