using R3;
using UnityEngine;

public class SFXPlayerPoolSlingshotShooterMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly SlingshotShooter _shooter;
    private readonly SlingshotShooterView _shooterView;

    public SFXPlayerPoolSlingshotShooterMediator(SFXPlayerObjectPool playerObjectPool,
        SlingshotShooter slingshotShooter,
        SlingshotShooterView slingshotShooterView)
    {
        _playerObjectPool = playerObjectPool;
        _shooter = slingshotShooter;
        _shooterView = slingshotShooterView;
    }

    public override void Initialize()
    {
        Vector3 position = _shooterView.transform.position;

        _shooter.DraggingStarted
            .Subscribe(bird =>
            {
                _playerObjectPool.PlaySFX(position, bird.GetComponent<BirdFlyerView>().SelectionResource);
                _playerObjectPool.PlaySFX(position, _shooterView.DraggingResource);
            })
            .AddTo(CompositeDisposable);

        _shooter.Shot
            .Subscribe(bird =>
            {
                _playerObjectPool.PlaySFX(position, _shooterView.ShotResource);
                _playerObjectPool.PlaySFX(position, bird.GetComponent<BirdFlyerView>().FlyingResource);
            })
            .AddTo(CompositeDisposable);
    }
}
