using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using System;
using System.Threading;
using UnityEngine;

public class SlingshotBirdPlacer : IDisposable
{
    private readonly SlingshotShooter _shooter;
    private readonly SlingshotShooterView _shooterView;
    private readonly SlingshotPlacingSettings _placingSettings;
    private readonly CancellationTokenSource _cts = new();

    private bool _isPlacing = false;

    public SlingshotBirdPlacer(SlingshotShooter shooter,
        SlingshotShooterView shooterView,
        SlingshotPlacingSettings placingSettings)
    {
        _shooter = shooter;
        _shooterView = shooterView;
        _placingSettings = placingSettings;
    }

    public bool IsPlacing { get => _isPlacing; set => _isPlacing = value; }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public async UniTask PlaceBirdAsync(Rigidbody bird)
    {
        if (_isPlacing)
        {
            Debug.LogError("The bird is already being placed in the slingshot", bird);
            return;
        }

        _isPlacing = true;
        Transform birdTransform = bird.transform;

        await PlaySquashAsync(birdTransform, _cts.Token);

        UniTask jumpTask = PlayJumpAsync(birdTransform, _cts.Token);
        UniTask scaleTask = PlayJumpScaleAsync(birdTransform, _cts.Token);

        await UniTask.WhenAll(jumpTask, scaleTask);

        _shooter.SetCurrentBird(bird);
        _isPlacing = false;
    }

    private async UniTask PlaySquashAsync(Transform birdTransform, CancellationToken token)
    {
        await LMotion.Create(Vector3.one, _placingSettings.SquashScale, _placingSettings.SquashDuration)
            .WithEase(_placingSettings.SquashEase)
            .BindToLocalScale(birdTransform)
            .ToUniTask(token);
    }

    private async UniTask PlayJumpAsync(Transform birdTransform, CancellationToken token)
    {
        Vector3 startPosition = birdTransform.position;
        Vector3 endPosition = _shooterView.CenterAnchor.position;

        await LMotion.Create(0f, 1f, _placingSettings.PlacingDuration)
            .WithEase(_placingSettings.JumpEase)
            .Bind(value =>
            {
                Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, value);
                float parabolicOffset = ParabolicTrajectory.CalculateOffset(_placingSettings.JumpHeight, value);
                currentPosition.y += parabolicOffset;
                birdTransform.position = currentPosition;
            })
            .ToUniTask(token);
    }

    private async UniTask PlayJumpScaleAsync(Transform birdTransform, CancellationToken token)
    {
        float halfDuration = _placingSettings.PlacingDuration * 0.5f;

        await LMotion.Create(_placingSettings.SquashScale, _placingSettings.StretchScale, halfDuration)
            .WithEase(_placingSettings.RiseScaleEase)
            .BindToLocalScale(birdTransform)
            .ToUniTask(token);

        await LMotion.Create(_placingSettings.StretchScale, Vector3.one, halfDuration)
            .WithEase(_placingSettings.FallScaleEase)
            .BindToLocalScale(birdTransform)
            .ToUniTask(token);
    }
}
