using UnityEngine;

[CreateAssetMenu(fileName = "DestructionSettings", 
    menuName = "Scriptable Objects/DestructionSettings/DestructionSettings")]
public class DestructionSettings : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float MaxHealth { get; private set; } = 500f;
    [field: SerializeField] public PointsSettings PointsSettings { get; private set; }
    [field: SerializeField] public DestructionSFXSettings SfxSettings { get; private set; }
}
