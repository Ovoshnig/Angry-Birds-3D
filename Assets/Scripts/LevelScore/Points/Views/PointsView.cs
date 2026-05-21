using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private SerializableMotionSettings<float, NoOptions> _appearanceSettings;
    [SerializeField] private SerializableMotionSettings<float, NoOptions> _disappearanceSettings;

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

        _currentHandle = LMotion.Create(_appearanceSettings).BindToLocalScaleXYZ(transform);
        await _currentHandle.ToUniTask(destroyCancellationToken);

        _currentHandle = LMotion.Create(_disappearanceSettings).BindToLocalScaleXYZ(transform);
        await _currentHandle.ToUniTask(destroyCancellationToken);

        _completed.OnNext(Unit.Default);
    }
}
