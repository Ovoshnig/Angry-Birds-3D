using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Slingshot.Shooting;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;
using UnityEngine;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolSlingshotShooterMediator(SFXPlayerObjectPool playerObjectPool,
    SlingshotShooter slingshotShooter, SlingshotShooterView slingshotShooterView) : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool = playerObjectPool;
    private readonly SlingshotShooter _shooter = slingshotShooter;
    private readonly SlingshotShooterView _shooterView = slingshotShooterView;

    protected override void Bind(CompositeDisposable disposables)
    {
        Transform shooterTransform = _shooterView.transform;

        _shooter.DraggingStarted
            .Subscribe(bird =>
            {
                BirdSfxProfile birdSfxProfile = bird.GetComponent<BirdEntityView>().SfxProfile;
                _playerObjectPool.PlaySFX(bird.transform, birdSfxProfile.SelectionResource);
                _playerObjectPool.PlaySFX(shooterTransform, _shooterView.DraggingResource);
            })
            .AddTo(disposables);

        _shooter.Shot
            .Subscribe(bird =>
            {
                BirdSfxProfile birdSfxProfile = bird.GetComponent<BirdEntityView>().SfxProfile;
                _playerObjectPool.PlaySFX(shooterTransform, _shooterView.ShotResource);
                _playerObjectPool.PlaySFX(bird.transform, birdSfxProfile.FlyingResource);
            })
            .AddTo(disposables);
    }
}
