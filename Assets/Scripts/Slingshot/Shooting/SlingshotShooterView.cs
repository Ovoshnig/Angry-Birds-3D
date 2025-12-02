using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class SlingshotShooterView : MonoBehaviour
{
    public enum SlingshotState { Idle, Dragging, Flying }

    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private LineRenderer _leftRubber;
    [SerializeField] private LineRenderer _rightRubber;

    private readonly Collider[] _collisionBuffer = new Collider[4];
    private readonly Subject<Unit> _birdCollided = new();
    private readonly CompositeDisposable _dragDisposable = new();

    private SlingshotInputHandler _slingshotInputHandler;
    private SlingshotSettings _slingshotSettings;
    private BirdSettings _birdSettings;
    private Camera _mainCamera;
    private Rigidbody _currentBird;
    private SlingshotState _currentState = SlingshotState.Idle;

    private float _birdRadius = 0.5f;
    private bool _hasCollided = false;

    public Observable<Unit> BirdCollided => _birdCollided;

    [Inject]
    public void Construct(SlingshotInputHandler pointerInputHandler,
        SlingshotSettings slingshotSettings,
        BirdSettings birdSettings)
    {
        _slingshotInputHandler = pointerInputHandler;
        _slingshotSettings = slingshotSettings;
        _birdSettings = birdSettings;
    }

    private void Awake()
    {
        _slingshotInputHandler.LeftButtonPressed
            .Subscribe(isPressed =>
            {
                if (isPressed)
                    OnPointerPressed();
                else
                    OnPointerReleased();
            })
            .AddTo(this);
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        _leftRubber.useWorldSpace = true;
        _rightRubber.useWorldSpace = true;
    }

    private void OnDestroy() => _dragDisposable.Dispose();

    private void Update()
    {
        if (_currentBird == null)
        {
            SetLinesActive(false);
            return;
        }

        if (_currentState == SlingshotState.Flying)
            HandleFlight();
    }

    public void SetCurrentBird(BirdFlyerView birdFlyerView)
    {
        SphereCollider birdCollider = birdFlyerView.GetComponent<SphereCollider>();
        _currentBird = birdCollider.attachedRigidbody;
        _birdRadius = birdCollider.radius;

        ResetBird();
    }

    private void OnPointerPressed()
    {
        if (_currentState == SlingshotState.Idle && IsPointerNear())
        {
            _currentState = SlingshotState.Dragging;
            _currentBird.isKinematic = true;

            _slingshotInputHandler.DragInput
                .Subscribe(_ =>
                {
                    HandleDrag();
                    UpdateRubberGeometry();
                })
                .AddTo(_dragDisposable);
        }
    }

    private void OnPointerReleased()
    {
        if (_currentState == SlingshotState.Dragging)
        {
            _currentState = SlingshotState.Flying;
            Shoot();
            SetLinesActive(false);

            _dragDisposable.Clear();
        }
    }

    private void HandleDrag()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        mouseWorldPosition.x = _centerPoint.position.x;

        Vector3 pullVector = mouseWorldPosition - _centerPoint.position;
        float distance = pullVector.magnitude;

        if (distance > _slingshotSettings.MaxDragDistance)
        {
            pullVector = pullVector.normalized * _slingshotSettings.MaxDragDistance;
            distance = _slingshotSettings.MaxDragDistance;
        }

        if (Physics.Raycast(_centerPoint.position,
            pullVector.normalized,
            out RaycastHit hit,
            distance,
            _slingshotSettings.SlingshotLayer))
        {
            float safeDistance = Mathf.Max(0,
                hit.distance - (_birdRadius + _slingshotSettings.SlingshotCollisionOffset));
            pullVector = pullVector.normalized * safeDistance;
        }

        _currentBird.transform.position = _centerPoint.position + pullVector;
        _currentBird.transform.forward = -pullVector.normalized;
    }

    private void UpdateRubberGeometry()
    {
        SetLinesActive(true);

        Vector3 birdPosition = _currentBird.transform.position;
        Vector3 anchorCenter = _centerPoint.position;

        Vector3 pullDirection = (birdPosition - anchorCenter);
        pullDirection.Normalize();

        Vector3 rubberRight = Vector3.Cross(pullDirection, Vector3.up).normalized;

        if (Vector3.Dot(rubberRight, _centerPoint.right) < 0)
            rubberRight = -rubberRight;

        Vector3 rubberLeft = -rubberRight;

        float totalRadius = _birdRadius + _slingshotSettings.SkinOffset;
        Vector3 pouchPoint = birdPosition + (pullDirection * totalRadius);

        Vector3 wrapOffsetVector = _slingshotSettings.RubberWrapOffset * totalRadius * pullDirection;
        float scaleOffset = _slingshotSettings.TangentScale * totalRadius;

        Vector3 leftControl = birdPosition + (scaleOffset * rubberLeft) + wrapOffsetVector;
        Vector3 rightControl = birdPosition + (scaleOffset * rubberRight) + wrapOffsetVector;

        DrawTightCurve(_leftRubber, _leftAnchor.position, pouchPoint, leftControl);
        DrawTightCurve(_rightRubber, _rightAnchor.position, pouchPoint, rightControl);
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
        _currentState = SlingshotState.Flying;
        _currentBird.isKinematic = false;

        _hasCollided = false;

        SetLinesActive(false);

        Vector3 force = _centerPoint.position - _currentBird.transform.position;
        _currentBird.AddForce(force * _slingshotSettings.LaunchForce, ForceMode.Impulse);
    }

    private void HandleFlight()
    {
        if (_currentBird == null || _hasCollided)
            return;

        if (_currentBird.linearVelocity.sqrMagnitude > _birdSettings.RotationSpeedThreshold)
            _currentBird.transform.forward = _currentBird.linearVelocity.normalized;

        int hitCount = Physics.OverlapSphereNonAlloc(_currentBird.transform.position,
            _birdRadius,
            _collisionBuffer);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = _collisionBuffer[i];

            if (hit.transform == _currentBird.transform || hit.isTrigger)
                continue;

            _hasCollided = true;
            _currentBird = null;
            _birdCollided.OnNext(Unit.Default);
            break;
        }
    }

    private void ResetBird()
    {
        _currentState = SlingshotState.Idle;
        _currentBird.isKinematic = true;
        _currentBird.transform.SetPositionAndRotation(_centerPoint.position, Quaternion.identity);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        float z = _mainCamera.WorldToScreenPoint(_centerPoint.position).z;
        return _mainCamera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, z));
    }

    private bool IsPointerNear()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        mousePosition.x = _centerPoint.position.x;
        return Vector3.Distance(mousePosition, _centerPoint.position)
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
