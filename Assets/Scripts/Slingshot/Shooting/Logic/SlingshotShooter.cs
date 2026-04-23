using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

public class SlingshotShooter : IInitializable, IDisposable
{
    public enum SlingshotState { Idle, Dragging, Flying }

    private readonly SlingshotInputHandler _slingshotInputHandler;
    private readonly SlingshotSettings _slingshotSettings;
    private readonly LineRenderer _leftRubber;
    private readonly LineRenderer _rightRubber;
    private readonly Transform _centerAnchorTransform;
    private readonly Vector3 _leftAnchorPosition;
    private readonly Vector3 _rightAnchorPosition;
    private readonly Vector3 _centerAnchorPosition;
    private readonly ReactiveProperty<SlingshotState> _currentState = new(SlingshotState.Idle);
    private readonly Subject<Rigidbody> _draggingStarted = new();
    private readonly Subject<Rigidbody> _shot = new();
    private readonly CompositeDisposable _leftButtonDisposable = new();
    private readonly CompositeDisposable _dragDisposable = new();

    private Camera _mainCamera;
    private Rigidbody _currentBird = null;

    private float _birdRadius = 0.5f;

    public SlingshotShooter(SlingshotInputHandler slingshotInputHandler,
        SlingshotSettings slingshotSettings,
        Transform centerAnchorTransform,
        Vector3 leftAnchorPosition,
        Vector3 rightAnchorPosition,
        Vector3 centerAnchorPosition,
        LineRenderer leftRubber,
        LineRenderer rightRubber)
    {
        _slingshotInputHandler = slingshotInputHandler;
        _slingshotSettings = slingshotSettings;
        _centerAnchorTransform = centerAnchorTransform;
        _leftAnchorPosition = leftAnchorPosition;
        _rightAnchorPosition = rightAnchorPosition;
        _centerAnchorPosition = centerAnchorPosition;
        _leftRubber = leftRubber;
        _rightRubber = rightRubber;
    }

    public ReadOnlyReactiveProperty<SlingshotState> CurrentState => _currentState;
    public Observable<Rigidbody> DraggingStarted => _draggingStarted;
    public Observable<Rigidbody> Shot => _shot;

    public void Initialize()
    {
        _mainCamera = Camera.main;

        _slingshotInputHandler.LeftButtonPressed
            .Subscribe(isPressed =>
            {
                if (isPressed)
                    OnPointerPressed();
                else
                    OnPointerReleased();
            })
            .AddTo(_leftButtonDisposable);
    }

    public void Dispose()
    {
        _leftButtonDisposable.Dispose();
        _dragDisposable.Dispose();
    }

    public void SetCurrentBird(Rigidbody birdRigidbody)
    {
        _currentBird = birdRigidbody;
        _birdRadius = birdRigidbody.GetComponent<SphereCollider>().radius;

        ResetBird();
    }

    private void OnPointerPressed()
    {
        if (_currentState.Value == SlingshotState.Idle && IsPointerNear())
        {
            _currentState.Value = SlingshotState.Dragging;
            _currentBird.isKinematic = true;

            _slingshotInputHandler.DragInput
                .Subscribe(_ =>
                {
                    HandleDrag();
                    UpdateRubberGeometry();
                })
                .AddTo(_dragDisposable);

            _slingshotInputHandler.DragInput
                .Take(1)
                .Subscribe(_ => _draggingStarted.OnNext(_currentBird))
                .AddTo(_dragDisposable);
        }
    }

    private void OnPointerReleased()
    {
        if (_currentState.Value == SlingshotState.Dragging)
        {
            _currentState.Value = SlingshotState.Flying;
            Shoot();
            SetLinesActive(false);

            _dragDisposable.Clear();
        }
    }

    private void HandleDrag()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        mouseWorldPosition.x = _centerAnchorPosition.x;

        Vector3 pullVector = mouseWorldPosition - _centerAnchorPosition;
        float distance = pullVector.magnitude;

        if (distance > _slingshotSettings.MaxDragDistance)
        {
            pullVector = pullVector.normalized * _slingshotSettings.MaxDragDistance;
            distance = _slingshotSettings.MaxDragDistance;
        }

        if (Physics.Raycast(_centerAnchorPosition,
            pullVector.normalized,
            out RaycastHit hit,
            distance,
            _slingshotSettings.SlingshotLayer))
        {
            float safeDistance = Mathf.Max(0,
                hit.distance - (_birdRadius + _slingshotSettings.SlingshotCollisionOffset));
            pullVector = pullVector.normalized * safeDistance;
        }

        _currentBird.transform.position = _centerAnchorPosition + pullVector;
        _currentBird.transform.forward = -pullVector.normalized;
    }

    private void UpdateRubberGeometry()
    {
        SetLinesActive(true);

        Vector3 birdPosition = _currentBird.transform.position;

        Vector3 pullDirection = (birdPosition - _centerAnchorPosition);
        pullDirection.Normalize();

        Vector3 rubberRight = Vector3.Cross(pullDirection, Vector3.up).normalized;

        if (Vector3.Dot(rubberRight, _centerAnchorTransform.right) < 0)
            rubberRight = -rubberRight;

        Vector3 rubberLeft = -rubberRight;

        float totalRadius = _birdRadius + _slingshotSettings.SkinOffset;
        Vector3 pouchPoint = birdPosition + (pullDirection * totalRadius);

        Vector3 wrapOffsetVector = _slingshotSettings.RubberWrapOffset * totalRadius * pullDirection;
        float scaleOffset = _slingshotSettings.TangentScale * totalRadius;

        Vector3 leftControl = birdPosition + (scaleOffset * rubberLeft) + wrapOffsetVector;
        Vector3 rightControl = birdPosition + (scaleOffset * rubberRight) + wrapOffsetVector;

        DrawTightCurve(_leftRubber, _leftAnchorPosition, pouchPoint, leftControl);
        DrawTightCurve(_rightRubber, _rightAnchorPosition, pouchPoint, rightControl);
    }

    private void DrawTightCurve(LineRenderer lineRenderer, Vector3 start, Vector3 end, Vector3 control)
    {
        lineRenderer.positionCount = _slingshotSettings.SegmentCount;

        for (int i = 0; i < _slingshotSettings.SegmentCount; i++)
        {
            float t = (float)i / (_slingshotSettings.SegmentCount - 1);
            float u = 1 - t;
            Vector3 point = (u * u * start) + (2 * u * t * control) + (t * t * end);
            lineRenderer.SetPosition(i, point);
        }
    }

    private void Shoot()
    {
        _currentState.Value = SlingshotState.Flying;
        _currentBird.isKinematic = false;

        SetLinesActive(false);

        Vector3 force = _centerAnchorPosition - _currentBird.transform.position;
        _currentBird.AddForce(force * _slingshotSettings.LaunchForce, ForceMode.Impulse);

        _shot.OnNext(_currentBird);
        _currentBird = null;
    }

    private void ResetBird()
    {
        _currentState.Value = SlingshotState.Idle;
        _currentBird.isKinematic = true;
        _currentBird.transform.SetPositionAndRotation(_centerAnchorPosition, Quaternion.identity);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        float z = _mainCamera.WorldToScreenPoint(_centerAnchorPosition).z;
        return _mainCamera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, z));
    }

    private bool IsPointerNear()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        mousePosition.x = _centerAnchorPosition.x;
        return Vector3.Distance(mousePosition, _centerAnchorPosition)
            <= _slingshotSettings.InputInteractionRadius;
    }

    private void SetLinesActive(bool active)
    {
        if (_leftRubber.enabled != active)
        {
            _leftRubber.enabled = active;
            _rightRubber.enabled = active;
        }
    }
}
