using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    private readonly Subject<Unit> _completed = new();

    private TMP_Text _text;
    private Camera _camera;
    private MotionHandle _currentHandle;

    public Observable<Unit> Completed => _completed;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _camera = Camera.main;
    }

    private void OnDestroy() => _completed.Dispose();

    public async UniTask ShowAsync(Vector3 position, PointsSettings pointsSettings)
    {
        _currentHandle.TryCancel();

        transform.SetPositionAndRotation(position, _camera.transform.rotation);

        _text.SetText("{0}", pointsSettings.Points);
        _text.color = pointsSettings.Color;
        _text.fontSize = pointsSettings.FontSize;

        _currentHandle = LMotion.Create(0f, 1f, pointsSettings.AppearanceDuration)
            .WithEase(pointsSettings.AppearanceEase)
            .BindToLocalScaleXYZ(transform);

        await _currentHandle.ToUniTask(destroyCancellationToken);
        await UniTask.WaitForSeconds(pointsSettings.ShowingDuration, cancellationToken: destroyCancellationToken);

        _currentHandle = LMotion.Create(1f, 0f, pointsSettings.DisappearanceDuration)
            .WithEase(pointsSettings.DisappearanceEase)
            .BindToLocalScaleXYZ(transform);

        await _currentHandle.ToUniTask(destroyCancellationToken);

        _completed.OnNext(Unit.Default);
    }
}
