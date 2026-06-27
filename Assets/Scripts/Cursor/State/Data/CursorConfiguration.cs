using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CursorConfiguration", menuName = "Scriptable Objects/Cursor Configuration")]
public sealed class CursorConfiguration : ScriptableObject, ISerializationCallbackReceiver
{
    [field: SerializeField] public List<CursorData> CursorPresets { get; private set; } = new();

    private readonly Dictionary<CursorState, CursorData> _cursorMap = new();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        _cursorMap.Clear();

        foreach (CursorData preset in CursorPresets)
            _cursorMap[preset.State] = preset;
    }

    public bool TryGetCursorData(CursorState state, out CursorData data) =>
        _cursorMap.TryGetValue(state, out data);
}
