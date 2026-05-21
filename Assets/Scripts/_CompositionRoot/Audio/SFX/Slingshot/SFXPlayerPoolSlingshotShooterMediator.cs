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

    protected override void Bind(CompositeDisposable disposables)
    {
        Transform shooterTransform = _shooterView.transform;

        _shooter.DraggingStarted
            .Subscribe(bird =>
            {
                BirdSFXSettings birdSfxSettings = bird.GetComponent<BirdEntityView>().SFXSettings;

                _playerObjectPool.PlaySFX(bird.transform, birdSfxSettings.SelectionResource);
                _playerObjectPool.PlaySFX(shooterTransform, _shooterView.DraggingResource);
            })
            .AddTo(disposables);

        _shooter.Shot
            .Subscribe(bird =>
            {
                BirdSFXSettings birdSfxSettings = bird.GetComponent<BirdEntityView>().SFXSettings;

                _playerObjectPool.PlaySFX(shooterTransform, _shooterView.ShotResource);
                _playerObjectPool.PlaySFX(bird.transform, birdSfxSettings.FlyingResource);
            })
            .AddTo(disposables);
    }
}
