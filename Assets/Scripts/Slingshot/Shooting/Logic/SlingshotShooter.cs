using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

public class SlingshotShooter : IStartable, IDisposable
{
    public enum SlingshotState { Idle, Dragging, Flying }

    private readonly SlingshotInputProvider _slingshotInputProvider;
    private readonly SlingshotShooterView _shooterView;
    private readonly SlingshotSettings _slingshotSettings;
    private readonly Camera _mainCamera;
    private readonly ReactiveProperty<SlingshotState> _currentState = new(SlingshotState.Idle);
    private readonly Subject<Rigidbody> _draggingStarted = new();
    private readonly Subject<Rigidbody> _shot = new();
    private readonly CompositeDisposable _leftButtonDisposable = new();
    private readonly CompositeDisposable _dragDisposable = new();

    private Rigidbody _currentBird = null;
    private Vector3 _centerAnchorPosition = Vector3.zero;
    private float _birdRadius = 0.5f;

    public SlingshotShooter(SlingshotInputProvider slingshotInputProvider,
        SlingshotShooterView shooterView,
        SlingshotSettings slingshotSettings)
    {
        _slingshotInputProvider = slingshotInputProvider;
        _shooterView = shooterView;
        _slingshotSettings = slingshotSettings;

        _centerAnchorPosition = shooterView.CenterAnchor.transform.position;

        _mainCamera = Camera.main;
    }

    public ReadOnlyReactiveProperty<SlingshotState> CurrentState => _currentState;
    public Observable<Rigidbody> DraggingStarted => _draggingStarted;
    public Observable<Rigidbody> Shot => _shot;

    public void Start()
    {
        _shooterView.LeftRubber.positionCount = _slingshotSettings.SegmentCount;
        _shooterView.RightRubber.positionCount = _slingshotSettings.SegmentCount;

        _slingshotInputProvider.LeftButtonPressed
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

        _currentState.Dispose();
        _draggingStarted.Dispose();
        _shot.Dispose();
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

            _slingshotInputProvider.DragInput
                .Subscribe(_ =>
                {
                    HandleDrag();
                    UpdateRubberGeometry();
                })
                .AddTo(_dragDisposable);

            _slingshotInputProvider.DragInput
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
            float safeDistance = Mathf.Max(0, hit.distance
                - (_birdRadius + _slingshotSettings.SlingshotCollisionOffset));
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

        if (Vector3.Dot(rubberRight, _shooterView.CenterAnchor.transform.right) < 0)
            rubberRight = -rubberRight;

        Vector3 rubberLeft = -rubberRight;

        float totalRadius = _birdRadius + _slingshotSettings.SkinOffset;
        Vector3 pouchPoint = birdPosition + (pullDirection * totalRadius);

        Vector3 wrapOffsetVector = _slingshotSettings.RubberWrapOffset * totalRadius * pullDirection;
        float scaleOffset = _slingshotSettings.TangentScale * totalRadius;

        Vector3 leftControl = birdPosition + (scaleOffset * rubberLeft) + wrapOffsetVector;
        Vector3 rightControl = birdPosition + (scaleOffset * rubberRight) + wrapOffsetVector;

        Vector3 leftRubberPosition = _shooterView.LeftRubber.transform.position;
        Vector3 rightRubberPosition = _shooterView.RightRubber.transform.position;

        DrawTightCurve(_shooterView.LeftRubber, leftRubberPosition, pouchPoint, leftControl);
        DrawTightCurve(_shooterView.RightRubber, rightRubberPosition, pouchPoint, rightControl);
    }

    private void DrawTightCurve(LineRenderer lineRenderer, Vector3 start, Vector3 end, Vector3 control)
    {
        for (int i = 0; i < _slingshotSettings.SegmentCount; i++)
        {
            float t = (float)i / (_slingshotSettings.SegmentCount - 1);
            float u = 1 - t;
            Vector3 position = (u * u * start) + (2 * u * t * control) + (t * t * end);
            lineRenderer.SetPosition(i, position);
        }
    }

    private void Shoot()
    {
        _currentState.Value = SlingshotState.Flying;
        _currentBird.isKinematic = false;
        _currentBird.detectCollisions = true;

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
        if (_shooterView.LeftRubber.enabled != active)
        {
            _shooterView.LeftRubber.enabled = active;
            _shooterView.RightRubber.enabled = active;
        }
    }
}
