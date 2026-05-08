using UnityEngine;
using UnityEngine.Audio;

public class SlingshotShooterView : MonoBehaviour
{
    [SerializeField] private LineRenderer _leftRubber;
    [SerializeField] private LineRenderer _rightRubber;

    [field: SerializeField] public Transform CenterAnchor { get; private set; }
    [field: SerializeField] public AudioClip DraggingResource { get; private set; }
    [field: SerializeField] public AudioResource ShotResource { get; private set; }

    private SlingshotSettings _settings;
    private float _birdRadius;

    public void SetSettings(SlingshotSettings settings)
    {
        _settings = settings;
        _leftRubber.positionCount = _settings.SegmentCount;
        _rightRubber.positionCount = _settings.SegmentCount;
    }

    public void SetBirdRadius(float radius) => _birdRadius = radius;

    public void SetLinesVisibility(bool isVisible)
    {
        if (_leftRubber.enabled == isVisible)
            return;

        _leftRubber.enabled = isVisible;
        _rightRubber.enabled = isVisible;
    }

    public void UpdateRubbers(Vector3 birdPosition)
    {
        SetLinesVisibility(true);

        Vector3 pullDirection = (birdPosition - CenterAnchor.position).normalized;
        Vector3 rubberRight = Vector3.Cross(pullDirection, Vector3.up).normalized;

        if (Vector3.Dot(rubberRight, CenterAnchor.right) < 0)
            rubberRight = -rubberRight;

        Vector3 rubberLeft = -rubberRight;

        float totalRadius = _birdRadius + _settings.SkinOffset;
        Vector3 pouchPoint = birdPosition + (pullDirection * totalRadius);

        Vector3 wrapOffset = _settings.RubberWrapOffset * totalRadius * pullDirection;
        float scaleOffset = _settings.TangentScale * totalRadius;

        Vector3 leftControl = birdPosition + (scaleOffset * rubberLeft) + wrapOffset;
        Vector3 rightControl = birdPosition + (scaleOffset * rubberRight) + wrapOffset;

        DrawTightCurve(_leftRubber, _leftRubber.transform.position, pouchPoint, leftControl);
        DrawTightCurve(_rightRubber, _rightRubber.transform.position, pouchPoint, rightControl);
    }

    private void DrawTightCurve(LineRenderer lineRenderer, Vector3 start, Vector3 end, Vector3 control)
    {
        for (int i = 0; i < _settings.SegmentCount; i++)
        {
            float t = (float)i / (_settings.SegmentCount - 1);
            float u = 1 - t;
            Vector3 position = (u * u * start) + (2 * u * t * control) + (t * t * end);
            lineRenderer.SetPosition(i, position);
        }
    }
}
