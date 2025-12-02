public class SlingshotShooterMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly SlingshotShooterView _slingshotShooterView;

    public SlingshotShooterMediator(SlingshotShooter slingshotShooter,
        SlingshotShooterView slingshotShooterView)
    {
        _slingshotShooter = slingshotShooter;
        _slingshotShooterView = slingshotShooterView;
    }

    public override void Initialize()
    {
        _slingshotShooter.SetAnchorPositions(_slingshotShooterView.LeftAnchor.position,
            _slingshotShooterView.RightAnchor.position,
            _slingshotShooterView.CenterAnchor.position);

        _slingshotShooter.SetCenterAnchorTransform(_slingshotShooterView.CenterAnchor);

        _slingshotShooter.SetRubbers(_slingshotShooterView.LeftRubber,
            _slingshotShooterView.RightRubber);
    }
}
