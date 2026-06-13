using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake() => Rigidbody = GetComponent<Rigidbody>();

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
                .BindToLocalScale(transform)
                .ToUniTask(token);
        }
        catch (OperationCanceledException)
        {
            if (this == null)
                return;

            await LMotion.Create(transform.localScale, Vector3.one, settings.StretchCancelDuration)
                .WithEase(Ease.InQuad)
                .BindToLocalScale(transform)
                .ToUniTask(cancellationToken: destroyCancellationToken);
        }
    }

    public void LookAtVelocityDirection()
    {
        if (Rigidbody.linearVelocity.sqrMagnitude != 0f)
            Rigidbody.transform.forward = Rigidbody.linearVelocity.normalized;
    }
}
