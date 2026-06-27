using UnityEngine;

[CreateAssetMenu(fileName = "CursorConfiguration", menuName = "Scriptable Objects/Cursor Configuration")]
public sealed class CursorConfiguration : ScriptableObject
{
    [field: SerializeField] public Texture2D UiHoverTexture { get; private set; }
    [field: SerializeField] public Vector2 UiHoverHotspot { get; private set; }

    [field: SerializeField] public Texture2D UiClickTexture { get; private set; }
    [field: SerializeField] public Vector2 UiClickHotspot { get; private set; }

    [field: SerializeField] public Texture2D GameplayHoverTexture { get; private set; }
    [field: SerializeField] public Vector2 GameplayHoverHotspot { get; private set; }

    [field: SerializeField] public Texture2D GameplayGrabTexture { get; private set; }
    [field: SerializeField] public Vector2 GameplayGrabHotspot { get; private set; }
}
