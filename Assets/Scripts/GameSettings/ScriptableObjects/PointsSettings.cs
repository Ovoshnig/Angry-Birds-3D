using LitMotion;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsSettings", menuName = "Scriptable Objects/PointsSettings")]
public class PointsSettings : ScriptableObject
{
    [field: SerializeField, Min(0)] public int Points { get; private set; } = 500;
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField, Min(0f)] public float FontSize { get; private set; } = 1f;
    [field: SerializeField, Min(0f)] public float AppearanceDuration { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float ShowingDuration { get; private set; } = 1f;
    [field: SerializeField, Min(0f)] public float DisappearanceDuration { get; private set; } = 0.5f;
    [field: SerializeField] public Ease AppearanceEase { get; private set; } = Ease.OutQuint;
    [field: SerializeField] public Ease DisappearanceEase { get; private set; } = Ease.InSine;

    public float TotalDuration => AppearanceDuration + ShowingDuration + DisappearanceDuration;
}
