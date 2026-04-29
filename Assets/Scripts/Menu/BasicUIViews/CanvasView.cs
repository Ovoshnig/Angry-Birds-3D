using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class CanvasView : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake() => _canvas = GetComponent<Canvas>();
}
