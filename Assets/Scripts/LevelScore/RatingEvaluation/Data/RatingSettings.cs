using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelRatingSettings", menuName = "Scriptable Objects/Level Rating Settings")]
public class RatingSettings : ScriptableObject
{
    [SerializeField] private Dictionary<int, int> _levelMaxScores = new();

    [field: SerializeField, Min(0)] public int MinStarCount { get; private set; } = 1;
    [field: SerializeField, Min(0)] public int MaxStarCount { get; private set; } = 3;

    public IReadOnlyDictionary<int, int> LevelMaxScores => _levelMaxScores;
}
