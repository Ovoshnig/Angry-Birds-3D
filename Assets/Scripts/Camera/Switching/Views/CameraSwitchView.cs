using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitchView : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _slingshotCamera;
    [SerializeField] private CinemachineCamera _generalCamera;
    [SerializeField] private CinemachineCamera _structureCamera;

    private CinemachineCamera _activeCamera = null;

    public void SetPrioritySlingshot() => SetPriority(_slingshotCamera);

    public void SetPriorityGeneral() => SetPriority(_generalCamera);

    public void SetPriorityStructure() => SetPriority(_structureCamera);

    private void SetPriority(CinemachineCamera camera)
    {
        if (_activeCamera != null)
            _activeCamera.Priority = 0;

        camera.Priority = 1;
        _activeCamera = camera;
    }
}
