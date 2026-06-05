using UnityEngine;

[CreateAssetMenu(fileName = "LevelRatingSettings",
    menuName = "Scriptable Objects/LevelRatingSettings")]
public class RatingSettings : ScriptableObject
{
    [field: SerializeField, Min(0)] public int MinStarCount { get; private set; } = 1;
    [field: SerializeField, Min(0)] public int MaxStarCount { get; private set; } = 3;
    [field: SerializeField] public int[] LevelMaxScoreThresholds { get; private set; }
}
