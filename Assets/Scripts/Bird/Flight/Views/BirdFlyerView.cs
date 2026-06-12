using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake() => Rigidbody = GetComponent<Rigidbody>();

    public async UniTask StretchAsync(BirdStretchSettings settings)
    {
        await UniTask.WaitForSeconds(settings.StretchDelay, cancellationToken: destroyCancellationToken);

        float velocityFactor = Mathf.InverseLerp(
            settings.MinVelocitySquareMagnitude,
            settings.MaxVelocitySquareMagnitude,
            Rigidbody.linearVelocity.sqrMagnitude);

        Vector3 stretchScale = Vector3.Lerp(Vector3.one, settings.MaxStretchScale, velocityFactor);

        await LMotion.Create(Vector3.one, stretchScale, settings.StretchDuration)
            .WithEase(settings.StretchEase)
            .WithLoops(2, LoopType.Flip)
            .BindToLocalScale(transform)
            .ToUniTask(cancellationToken: destroyCancellationToken);
    }

    public void LookAtVelocityDirection()
    {
        if (Rigidbody.linearVelocity.sqrMagnitude != 0f)
            Rigidbody.transform.forward = Rigidbody.linearVelocity.normalized;
    }
}
