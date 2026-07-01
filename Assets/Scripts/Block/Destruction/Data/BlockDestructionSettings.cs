using LitMotion;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockDestructionSettings",
    menuName = "Scriptable Objects/Block Destruction Settings")]
public class BlockDestructionSettings : ScriptableObject
{
    [field: SerializeField] public float Duration { get; private set; } = 0.4f;
    [field: SerializeField] public Ease Ease { get; private set; } = Ease.OutQuad;
}
