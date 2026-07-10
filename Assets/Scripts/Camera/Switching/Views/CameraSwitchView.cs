using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraSwitchView : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _slingshotCamera;
    [SerializeField] private CinemachineCamera _generalCamera;
    [SerializeField] private CinemachineCamera _structureCamera;

    private readonly ReactiveProperty<bool> _isBlending = new(false);

    private CinemachineBrain _brain;
    private CinemachineCamera _activeCamera = null;
    private CancellationTokenSource _cts = null;

    public ReadOnlyReactiveProperty<bool> IsBlending => _isBlending;

    private CinemachineCamera ActiveCamera
    {
        get
        {
            if (_activeCamera == null)
                _activeCamera = _brain.ActiveVirtualCamera as CinemachineCamera;

            return _activeCamera;
        }

        set => _activeCamera = value;
    }

    private void Awake() => _brain = GetComponent<CinemachineBrain>();

    private void OnDestroy()
    {
        CancelCts();
        _isBlending.Dispose();
    }

    public UniTask SwitchToSlingshotAsync() => SwitchAndAwaitBlendAsync(_slingshotCamera);

    public UniTask SwitchToGeneralAsync() => SwitchAndAwaitBlendAsync(_generalCamera);

    public UniTask SwitchToStructureAsync() => SwitchAndAwaitBlendAsync(_structureCamera);

    private async UniTask SwitchAndAwaitBlendAsync(CinemachineCamera targetCamera)
    {
        if (ActiveCamera == targetCamera)
            return;

        CancelCts();
        _cts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);
        CancellationToken cancellationToken = _cts.Token;

        SetPriority(targetCamera);
        _isBlending.Value = true;

        await UniTask.WaitUntil(() => _brain.IsBlending, cancellationToken: cancellationToken);
        await UniTask.WaitWhile(() => _brain.IsBlending, cancellationToken: cancellationToken);

        _isBlending.Value = false;
    }

    private void CancelCts()
    {
        if (_cts == null)
            return;

        _cts.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    private void SetPriority(CinemachineCamera camera)
    {
        ActiveCamera.Priority = 0;
        camera.Priority = 1;
        ActiveCamera = camera;
    }
}
