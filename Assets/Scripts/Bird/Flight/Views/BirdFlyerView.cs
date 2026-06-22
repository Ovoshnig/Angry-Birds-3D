using Cysharp.Threading.Tasks;
using LitMotion;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
    private readonly HashSet<BirdFlyerView> _cloneFlyerViews = new();

    public Rigidbody Rigidbody { get; private set; }

    private void Awake() => Rigidbody = GetComponent<Rigidbody>();

    public void AddClone(BirdFlyerView clone)
    {
        if (clone != null)
            _cloneFlyerViews.Add(clone);
    }

    public async UniTask StretchAsync(BirdStretchSettings settings, CancellationToken token)
    {
        try
        {
            await UniTask.WaitForSeconds(settings.StretchDelay, cancellationToken: token);

            float velocityFactor = Mathf.InverseLerp(
                settings.MinVelocitySquareMagnitude,
                settings.MaxVelocitySquareMagnitude,
                Rigidbody.linearVelocity.sqrMagnitude);

            Vector3 stretchScale = Vector3.Lerp(Vector3.one, settings.MaxStretchScale, velocityFactor);

            await LMotion.Create(Vector3.one, stretchScale, settings.StretchDuration)
                .WithEase(settings.StretchEase)
                .WithLoops(2, LoopType.Yoyo)
                .Bind(UpdateLocalScale)
                .ToUniTask(token);
        }
        catch (OperationCanceledException)
        {
            if (this == null)
                return;

            await LMotion.Create(transform.localScale, Vector3.one, settings.StretchCancelDuration)
                .WithEase(Ease.InQuad)
                .Bind(UpdateLocalScale)
                .ToUniTask(cancellationToken: destroyCancellationToken);
        }
    }

    public void UpdateLocalScale(Vector3 scale)
    {
        transform.localScale = scale;

        foreach (var clone in _cloneFlyerViews)
            clone.transform.localScale = scale;
    }

    public void LookAtVelocityDirection()
    {
        if (Rigidbody.linearVelocity.sqrMagnitude != 0f)
            Rigidbody.transform.forward = Rigidbody.linearVelocity.normalized;

        foreach (var clone in _cloneFlyerViews)
            clone.LookAtVelocityDirection();
    }
}
