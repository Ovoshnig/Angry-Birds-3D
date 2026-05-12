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
    private MotionHandle _handle;
    private Camera _camera;

    public Observable<Unit> Completed => _completed;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _camera = Camera.main;
    }

    private void OnDestroy() => _completed.Dispose();

    public void Show(Vector3 position, PointsSettings pointsSettings)
    {
        _handle.TryCancel();

        transform.SetPositionAndRotation(position, _camera.transform.rotation);

        _text.SetText("{0}", pointsSettings.Points);
        _text.color = pointsSettings.Color;
        _text.fontSize = pointsSettings.FontSize;

        _handle = LSequence.Create()
            .Append(LMotion.Create(_appearanceSettings).BindToLocalScaleXYZ(transform))
            .Append(LMotion.Create(_disappearanceSettings)
                .WithOnComplete(() => _completed.OnNext(Unit.Default))
                .BindToLocalScaleXYZ(transform))
            .Run()
            .AddTo(gameObject);
    }
}
