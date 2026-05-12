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

    public ReadOnlyReactiveProperty<bool> IsBlending => _isBlending;

    private void Awake() => _brain = GetComponent<CinemachineBrain>();

    private void OnDestroy() => _isBlending.Dispose();

    public UniTask SwitchToSlingshotAsync() => SwitchAndAwaitBlendAsync(_slingshotCamera);

    public UniTask SwitchToGeneralAsync() => SwitchAndAwaitBlendAsync(_generalCamera);

    public UniTask SwitchToStructureAsync() => SwitchAndAwaitBlendAsync(_structureCamera);

    private async UniTask SwitchAndAwaitBlendAsync(CinemachineCamera targetCamera)
    {
        if (_activeCamera == targetCamera)
            return;

        SetPriority(targetCamera);

        await UniTask.Yield(cancellationToken: destroyCancellationToken);
        _isBlending.Value = true;

        await UniTask.WaitWhile(() => _brain.IsBlending, cancellationToken: destroyCancellationToken);
        _isBlending.Value = false;
    }

    private void SetPriority(CinemachineCamera camera)
    {
        if (_activeCamera == null)
            _activeCamera = GetInitialActiveCamera();

        _activeCamera.Priority = 0;
        camera.Priority = 1;
        _activeCamera = camera;
    }

    private CinemachineCamera GetInitialActiveCamera()
    {
        if (_brain.IsLiveChild(_slingshotCamera))
            return _slingshotCamera;

        if (_brain.IsLiveChild(_structureCamera))
            return _structureCamera;

        return _generalCamera;
    }
}
