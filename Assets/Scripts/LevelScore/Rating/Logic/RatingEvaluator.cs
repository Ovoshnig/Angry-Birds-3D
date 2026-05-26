using UnityEngine;

public class RatingEvaluator
{
    private readonly ScoreModel _scoreModel;

    public RatingEvaluator(ScoreModel scoreModel) => _scoreModel = scoreModel;

    public int EvaluateStarCount(int maxScoreThreshold, int maxStarCount)
    {
        int oneStarThreshold = maxScoreThreshold / maxStarCount;
        int starCount = Mathf.FloorToInt(_scoreModel.Score.CurrentValue / oneStarThreshold);
        return Mathf.Clamp(starCount, 1, maxStarCount);
    }
}
