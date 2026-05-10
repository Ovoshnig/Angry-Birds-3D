using UnityEngine;

[CreateAssetMenu(fileName = "PointsSettings", menuName = "Scriptable Objects/PointsSettings")]
public class PointsSettings : ScriptableObject
{
    [field: SerializeField, Min(0)] public int Points { get; private set; } = 500;
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField, Min(0f)] public float FontSize { get; private set; } = 1f;
}
