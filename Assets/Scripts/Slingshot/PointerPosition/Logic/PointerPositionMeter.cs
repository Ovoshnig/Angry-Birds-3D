using UnityEngine;
using UnityEngine.InputSystem;

public class PointerPositionMeter
{
    private readonly Camera _mainCamera;

    public PointerPositionMeter() => _mainCamera = Camera.main;

    public Vector3 GetPointerWorldPosition(Vector3 anchorWorldPosition)
    {
        Vector2 pointerScreenPosition = Pointer.current.position.ReadValue();
        float zDepth = _mainCamera.WorldToScreenPoint(anchorWorldPosition).z;

        return _mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPosition.x, pointerScreenPosition.y, zDepth));
    }

    public bool IsPointerNear(Vector3 anchorPosition, float thresholdRadius)
    {
        Vector3 pointerPosition = GetPointerWorldPosition(anchorPosition);
        return Vector3.Distance(pointerPosition, anchorPosition) <= thresholdRadius;
    }
}
