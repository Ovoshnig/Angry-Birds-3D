using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class SlingshotShooter : IStartable, IDisposable, ITickable
{
    public enum SlingshotState { Idle, Dragging, Flying }

    private readonly SlingshotInputProvider _inputProvider;
    private readonly SlingshotShooterView _view;
    private readonly PointerPositionMeter _pointerMeter;
    private readonly SlingshotSettings _settings;
    private readonly ReactiveProperty<SlingshotState> _currentState = new(SlingshotState.Idle);
    private readonly Subject<Rigidbody> _shot = new();
    private readonly CompositeDisposable _disposables = new();

    private Rigidbody _currentBird;
    private Vector3 _centerAnchorPosition;
    private float _birdRadius;
    private bool _isDragInput;

    public SlingshotShooter(SlingshotInputProvider inputProvider,
        SlingshotShooterView view,
        PointerPositionMeter pointerMeter,
        SlingshotSettings settings)
    {
        _inputProvider = inputProvider;
        _view = view;
        _pointerMeter = pointerMeter;
        _settings = settings;

        DraggingStarted = _currentState
            .Pairwise()
            .Where(pair => pair.Previous != SlingshotState.Dragging
                && pair.Current == SlingshotState.Dragging)
            .Select(_ => _currentBird)
            .Share();
    }

    public ReadOnlyReactiveProperty<SlingshotState> CurrentState => _currentState;
    public Observable<Rigidbody> DraggingStarted { get; }
    public Observable<Rigidbody> Shot => _shot;

    public void Start()
    {
        _centerAnchorPosition = _view.CenterAnchor.position;
        _view.SetSettings(_settings);

        _inputProvider.LeftButtonPressed
            .Subscribe(HandlePointerState)
            .AddTo(_disposables);

        _inputProvider.DragInput
            .Subscribe(input => _isDragInput = input != Vector2.zero)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _currentState.Dispose();
        _shot.Dispose();
    }

    public void Tick()
    {
        if (_currentState.Value != SlingshotState.Dragging || !_isDragInput)
            return;

        UpdateBirdPosition();
        _view.UpdateRubbers(_currentBird.transform.position);
    }

    public void SetCurrentBird(Rigidbody birdRigidbody)
    {
        _currentBird = birdRigidbody;
        _birdRadius = birdRigidbody.GetComponent<SphereCollider>().radius;
        _view.SetBirdRadius(_birdRadius);

        ResetBird();
    }

    private void HandlePointerState(bool isPressed)
    {
        if (isPressed)
            OnPointerPressed();
        else
            OnPointerReleased();
    }

    private void OnPointerPressed()
    {
        if (_currentState.Value != SlingshotState.Idle
            || !_pointerMeter.IsPointerNear(_centerAnchorPosition, _settings.InputInteractionRadius))
            return;

        _currentState.Value = SlingshotState.Dragging;
        _isDragInput = false;
    }

    private void OnPointerReleased()
    {
        if (_currentState.Value != SlingshotState.Dragging)
            return;

        _currentState.Value = SlingshotState.Flying;
        _view.SetLinesVisibility(false);

        _currentBird.isKinematic = false;
        _currentBird.detectCollisions = true;

        Vector3 force = _centerAnchorPosition - _currentBird.transform.position;
        _currentBird.AddForce(force * _settings.LaunchForce, ForceMode.Impulse);

        _shot.OnNext(_currentBird);
        _currentBird = null;
    }

    private void UpdateBirdPosition()
    {
        Vector3 pointerPosition = _pointerMeter.GetPointerWorldPosition(_centerAnchorPosition);
        pointerPosition.x = _centerAnchorPosition.x;

        Vector3 pullVector = pointerPosition - _centerAnchorPosition;
        float distance = Mathf.Clamp(pullVector.magnitude, 0f, _settings.MaxDragDistance);

        pullVector = pullVector.normalized * distance;

        if (Physics.Raycast(_centerAnchorPosition, pullVector.normalized,
            out RaycastHit hit, distance, _settings.SlingshotLayer))
        {
            float safeDistance = Mathf.Max(0, hit.distance - (_birdRadius + _settings.SlingshotCollisionOffset));
            pullVector = pullVector.normalized * safeDistance;
        }

        _currentBird.transform.position = _centerAnchorPosition + pullVector;
        _currentBird.transform.forward = -pullVector.normalized;
    }

    private void ResetBird()
    {
        _currentState.Value = SlingshotState.Idle;
        _currentBird.isKinematic = true;
        _currentBird.transform.SetPositionAndRotation(_centerAnchorPosition, Quaternion.identity);
    }
}
