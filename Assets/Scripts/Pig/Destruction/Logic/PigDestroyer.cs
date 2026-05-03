public class PigDestroyer : ObjectDestroyer<PigEntityView>
{
    public PigDestroyer(PigCollider pigCollider) : base(pigCollider)
    {
    }

    protected override ObjectDestroyerView GetObjectDestroyerView(PigEntityView entityView) =>
        entityView.DestroyerView;
}
