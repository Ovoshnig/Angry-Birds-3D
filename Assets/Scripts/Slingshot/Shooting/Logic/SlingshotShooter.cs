using AngryBirds3D.Slingshot.Input;
using AngryBirds3D.Slingshot.PointerPosition;
using Ovoshnig.GameSettings;
using R3;
using System;
using UnityEngine;
using VContainer.Unity;

namespace AngryBirds3D.Slingshot.Shooting;

public class SlingshotShooter : IStartable, IDisposable, ITickable
{
    private readonly SlingshotInputProvider _inputProvider;
    private readonly SlingshotShooterView _view;
    private readonly PointerPositionMeter _pointerMeter;
    private readonly SlingshotSettings _settings;
    private readonly ReactiveProperty<SlingshotState> _currentState = new(SlingshotState.Idle);
    private readonly Subject<Rigidbody> _shot = new();
    private readonly CompositeDisposable _disposables = [];
    private Vector3 _centerAnchorPosition;
    private float _birdRadius;
    private bool _isDragInput;

    public SlingshotShooter(SlingshotInputProvider inputProvider, SlingshotShooterView view,
        PointerPositionMeter pointerMeter, SlingshotSettings settings)
    {
        _inputProvider = inputProvider;
        _view = view;
        _pointerMeter = pointerMeter;
        _settings = settings;

        DraggingStarted = _currentState
            .Pairwise()
            .Where(pair => pair.Previous != SlingshotState.Dragging
                && pair.Current == SlingshotState.Dragging)
            .Select(_ => CurrentBird)
            .Share();
    }

    public Rigidbody CurrentBird { get; private set; } = null;
    public ReadOnlyReactiveProperty<SlingshotState> CurrentState => _currentState;
    public Observable<Rigidbody> DraggingStarted { get; }
    public Observable<Rigidbody> Shot => _shot;
    public bool ContainsBird => CurrentBird != null;

    public void Start()
    {
        _centerAnchorPosition = _view.CenterAnchor.position;
        _view.SetSettings(_settings);
        SubscribeToInputProvider();
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
        _view.UpdateRubbers(CurrentBird.transform.position);
    }

    public void SetBird(Rigidbody birdRigidbody)
    {
        if (CurrentBird != null)
        {
            Debug.LogError("The bird is already placed in the slingshot", CurrentBird);
            return;
        }

        CurrentBird = birdRigidbody;
        _birdRadius = birdRigidbody.GetComponent<SphereCollider>().radius;
        _view.SetBirdRadius(_birdRadius);

        ResetBird();
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
            StopShooting();
        else
            SubscribeToInputProvider();
    }

    public void StopShooting()
    {
        _disposables.Clear();

        ResetBird();
        _view.SetLinesVisibility(false);
    }

    private void SubscribeToInputProvider()
    {
        _inputProvider.LeftButtonPressed
            .Subscribe(HandlePointerState)
            .AddTo(_disposables);

        _inputProvider.DragInput
            .Subscribe(input => _isDragInput = input != Vector2.zero)
            .AddTo(_disposables);
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
        if (_currentState.Value != SlingshotState.InputWaiting
            || !_pointerMeter.IsPointerNear(_centerAnchorPosition, _settings.InputInteractionRadius))
        {
            return;
        }

        _currentState.Value = SlingshotState.Dragging;
        _isDragInput = false;
    }

    private void OnPointerReleased()
    {
        if (_currentState.Value != SlingshotState.Dragging)
            return;

        _currentState.Value = SlingshotState.Idle;
        _view.SetLinesVisibility(false);

        CurrentBird.isKinematic = false;
        CurrentBird.detectCollisions = true;

        Vector3 force = _centerAnchorPosition - CurrentBird.transform.position;
        CurrentBird.AddForce(force * _settings.LaunchForce, ForceMode.Impulse);

        _shot.OnNext(CurrentBird);
        CurrentBird = null;
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

        CurrentBird.transform.position = _centerAnchorPosition + pullVector;
        CurrentBird.transform.forward = -pullVector.normalized;
    }

    private void ResetBird()
    {
        if (CurrentBird == null)
            return;

        _currentState.Value = SlingshotState.InputWaiting;
        CurrentBird.isKinematic = true;
        CurrentBird.transform.SetPositionAndRotation(_centerAnchorPosition, Quaternion.identity);
    }
}
