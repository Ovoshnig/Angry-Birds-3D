using UnityEngine;

[CreateAssetMenu(fileName = "DestructionProfile", menuName = "Scriptable Objects/Destruction/Profile")]
public class DestructionProfile : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float MaxHealth { get; private set; } = 15f;
    [field: SerializeField] public PointsSettings PointsSettings { get; private set; }
    [field: SerializeField] public DestructionSfxProfile SfxProfile { get; private set; }
}
