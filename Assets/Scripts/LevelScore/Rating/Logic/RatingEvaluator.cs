using R3;
using System;
using UnityEngine;

public class RatingEvaluator : IDisposable
{
    private readonly ScoreModel _scoreModel;
    private readonly Subject<int> _evaluated = new();

    public RatingEvaluator(ScoreModel scoreModel) => _scoreModel = scoreModel;

    public Observable<int> Evaluated => _evaluated;

    public void Dispose() => _evaluated.Dispose();

    public int EvaluateStarCount(int maxScoreThreshold, int maxStarCount)
    {
        int oneStarThreshold = maxScoreThreshold / maxStarCount;
        int starCount = Mathf.FloorToInt(_scoreModel.Score.CurrentValue / oneStarThreshold);
        int clampedStarCount = Mathf.Clamp(starCount, 1, maxStarCount);

        _evaluated.OnNext(clampedStarCount);
        return clampedStarCount;
    }
}
