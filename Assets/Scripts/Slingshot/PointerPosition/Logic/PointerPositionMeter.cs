using UnityEngine;
using UnityEngine.InputSystem;

public class PointerPositionMeter
{
    private readonly Camera _mainCamera;

    public PointerPositionMeter() => _mainCamera = Camera.main;

    public Vector3 GetPointerWorldPosition(Vector3 anchorPosition)
    {
        Vector2 pointerPosition = Pointer.current.position.ReadValue();
        float z = _mainCamera.ScreenToWorldPoint(anchorPosition).z;
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, z));

        return worldPosition;
    }

    public bool IsPointerNear(Vector3 anchorPosition, float thresholdRadius)
    {
        Vector3 pointerPosition = GetPointerWorldPosition(anchorPosition);
        return Vector3.Distance(pointerPosition, anchorPosition) <= thresholdRadius;
    }
}
