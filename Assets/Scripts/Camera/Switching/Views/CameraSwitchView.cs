using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraSwitchView : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _slingshotCamera;
    [SerializeField] private CinemachineCamera _generalCamera;
    [SerializeField] private CinemachineCamera _structureCamera;

    private CinemachineBrain _brain;
    private CinemachineCamera _activeCamera = null;

    private void Awake() => _brain = GetComponent<CinemachineBrain>();

    public UniTask SwitchToSlingshotAsync(CancellationToken token) => SwitchAndAwaitBlendAsync(_slingshotCamera, token);

    public UniTask SwitchToGeneralAsync(CancellationToken token) => SwitchAndAwaitBlendAsync(_generalCamera, token);

    public UniTask SwitchToStructureAsync(CancellationToken token) => SwitchAndAwaitBlendAsync(_structureCamera, token);

    private async UniTask SwitchAndAwaitBlendAsync(CinemachineCamera targetCamera, CancellationToken token)
    {
        if (_activeCamera == targetCamera)
            return;

        SetPriority(targetCamera);

        await UniTask.Yield(cancellationToken: token);
        await UniTask.WaitWhile(() => _brain.IsBlending, cancellationToken: token);
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
