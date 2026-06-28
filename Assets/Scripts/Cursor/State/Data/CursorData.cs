using System;
using UnityEngine;

[Serializable]
public struct CursorData
{
    [field: SerializeField] public CursorState State { get; private set; }
    [field: SerializeField] public Texture2D Texture { get; private set; }
    [field: SerializeField] public Vector2 Hotspot { get; private set; }
}
