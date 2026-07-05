using Cysharp.Threading.Tasks;
using LitMotion;
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
        await PlayJumpAndScaleAsync(birdTransform, _cts.Token);

        _shooter.SetCurrentBird(bird);
        _isPlacing = false;
    }

    private async UniTask PlaySquashAsync(Transform birdTransform, CancellationToken token)
    {
        SphereCollider collider = birdTransform.GetComponent<SphereCollider>();

        Vector3 startPosition = birdTransform.position;
        float groundY = collider.bounds.min.y;
        float localBottomOffset = startPosition.y - groundY;

        await LMotion.Create(Vector3.one, _placingSettings.SquashScale, _placingSettings.SquashDuration)
            .WithEase(_placingSettings.SquashEase)
            .Bind(scale =>
            {
                birdTransform.localScale = scale;

                Vector3 position = startPosition;
                position.y = groundY + (scale.y * localBottomOffset);
                birdTransform.position = position;
            })
            .ToUniTask(token);
    }

    private async UniTask PlayJumpAndScaleAsync(Transform birdTransform, CancellationToken token)
    {
        Vector3 startPosition = birdTransform.position;
        Vector3 endPosition = _shooterView.CenterAnchor.position;

        float peakProgress = ParabolicTrajectory.CalculatePeakProgress(
            startPosition.y,
            endPosition.y,
            _placingSettings.JumpHeight);

        float riseDuration = _placingSettings.PlacingDuration * peakProgress;
        float fallDuration = _placingSettings.PlacingDuration * (1f - peakProgress);

        await LMotion.Create(0f, 1f, riseDuration)
            .WithEase(_placingSettings.RiseEase)
            .Bind(value => UpdatePlacement(birdTransform, startPosition, endPosition,
                0f, peakProgress, _placingSettings.SquashScale, _placingSettings.StretchScale, value))
            .ToUniTask(token);

        await LMotion.Create(0f, 1f, fallDuration)
            .WithEase(_placingSettings.FallEase)
            .Bind(value => UpdatePlacement(birdTransform, startPosition, endPosition,
                peakProgress, 1f, _placingSettings.StretchScale, Vector3.one, value))
            .ToUniTask(token);
    }

    private void UpdatePlacement(Transform birdTransform, Vector3 start, Vector3 end,
        float startProgress, float endProgress, Vector3 startScale, Vector3 endScale, float value)
    {
        float progress = Mathf.Lerp(startProgress, endProgress, value);
        Vector3 position = Vector3.Lerp(start, end, progress);
        position.y += ParabolicTrajectory.CalculateOffset(_placingSettings.JumpHeight, progress);

        birdTransform.position = position;
        birdTransform.localScale = Vector3.Lerp(startScale, endScale, value);
    }
}
