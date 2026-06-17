using R3;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RatingEvaluator : IDisposable
{
    private readonly ScoreModel _scoreModel;
    private readonly RatingSettings _ratingSettings;
    private readonly SceneSettings _sceneSettings;
    private readonly ReactiveProperty<int> _rating = new();

    public RatingEvaluator(ScoreModel scoreModel,
        RatingSettings ratingSettings,
        SceneSettings sceneSettings)
    {
        _scoreModel = scoreModel;
        _ratingSettings = ratingSettings;
        _sceneSettings = sceneSettings;
    }

    public ReadOnlyReactiveProperty<int> Rating => _rating;

    public void Dispose() => _rating.Dispose();

    public void Evaluate()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex - _sceneSettings.FirstLevelIndex + 1;
        int maxScoreThreshold = _ratingSettings.LevelMaxScoreThresholds[currentLevelIndex];

        int oneStarThreshold = maxScoreThreshold / _ratingSettings.MaxStarCount;
        int starCount = Mathf.FloorToInt(_scoreModel.Score.CurrentValue / oneStarThreshold);
        int clampedStarCount = Mathf.Clamp(starCount, _ratingSettings.MinStarCount, _ratingSettings.MaxStarCount);
        _rating.Value = clampedStarCount;
    }
}
