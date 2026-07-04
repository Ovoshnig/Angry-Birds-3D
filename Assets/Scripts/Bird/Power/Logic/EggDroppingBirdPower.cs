using R3;
using System;
using UnityEngine;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

public class EggDroppingBirdPower : IBirdPower, IDisposable
{
    private readonly BirdExploder _birdExploder;
    private readonly EggDroppingPowerSettings _powerSettings;
    private readonly Collider[] _colliders;
    private readonly Subject<EggEntityView> _eggDropped = new();
    private readonly CompositeDisposable _disposables = new();

    public EggDroppingBirdPower(BirdExploder birdExploder, EggDroppingPowerSettings powerSettings)
    {
        _birdExploder = birdExploder;
        _powerSettings = powerSettings;

        _colliders = new Collider[powerSettings.MaxExplosiveCount];
    }

    public BirdPowerType Type => BirdPowerType.EggDropping;
    public Observable<EggEntityView> EggDropped => _eggDropped;

    public void Activate(BirdEntityView birdEntityView)
    {
        EggEntityView eggEntityView = birdEntityView.GetComponentInChildren<EggEntityView>();

        Transform eggTransform = eggEntityView.transform;
        eggTransform.SetParent(null);
        eggTransform.rotation = Quaternion.identity;

        Rigidbody eggRigidbody = eggEntityView.Rigidbody;
        eggRigidbody.isKinematic = false;
        eggRigidbody.AddForce(_powerSettings.DropForce * Vector3.down, ForceMode.Impulse);
        _eggDropped.OnNext(eggEntityView);

        birdEntityView.FlyerView.Rigidbody.AddForce(_powerSettings.RecoilForce * Vector3.up,
            ForceMode.Impulse);

        eggEntityView.ColliderView.Collided
            .Take(1)
            .Subscribe(_ => OnEggCollided(eggEntityView, birdEntityView.SfxProfile.ExplosionResource))
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _eggDropped.Dispose();
    }

    private void OnEggCollided(EggEntityView eggEntityView, AudioResource explosionResource)
    {
        _birdExploder.Explode(eggEntityView.gameObject, _colliders, _powerSettings.ExplosionForce,
            _powerSettings.ExplosionRadius, _powerSettings.UpwardsModifier, explosionResource);

        Object.Destroy(eggEntityView.gameObject);
    }
}
