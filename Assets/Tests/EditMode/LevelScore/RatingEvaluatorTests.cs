using NUnit.Framework;
using R3;
using System;

[TestFixture]
public class RatingEvaluatorTests
{
    private const int ThreeStarMaxScore = 300;
    private const int ThreeStarCount = 3;

    private ScoreModel _scoreModel;
    private RatingEvaluator _ratingEvaluator;

    [SetUp]
    public void SetUp()
    {
        _scoreModel = new ScoreModel();
        _ratingEvaluator = new RatingEvaluator(_scoreModel);
    }

    [TearDown]
    public void TearDown()
    {
        _ratingEvaluator.Dispose();
        _scoreModel.Dispose();
    }

    [TestCase(0, 1)]
    [TestCase(50, 1)]
    [TestCase(99, 1)]
    [TestCase(100, 1)]
    [TestCase(199, 1)]
    [TestCase(200, 2)]
    [TestCase(299, 2)]
    [TestCase(300, 3)]
    [TestCase(1000, 3)]
    public void EvaluateStarCount_ThreeStars_ReturnsExpectedCount(int score, int expectedStarCount)
    {
        SetScore(score);

        int starCount = _ratingEvaluator.EvaluateStarCount(ThreeStarMaxScore, ThreeStarCount);

        Assert.AreEqual(expectedStarCount, starCount);
    }

    [TestCase(0, 1, 500, 5)]
    [TestCase(100, 1, 500, 5)]
    [TestCase(200, 2, 500, 5)]
    [TestCase(400, 4, 500, 5)]
    [TestCase(500, 5, 500, 5)]
    [TestCase(999, 5, 500, 5)]
    public void EvaluateStarCount_FiveStars_ReturnsExpectedCount(
        int score, int expectedStarCount, int maxScoreThreshold, int maxStarCount)
    {
        SetScore(score);

        int starCount = _ratingEvaluator.EvaluateStarCount(maxScoreThreshold, maxStarCount);

        Assert.AreEqual(expectedStarCount, starCount);
    }

    [TestCase(0, 1, 100, 1)]
    [TestCase(50, 1, 100, 1)]
    [TestCase(100, 1, 100, 1)]
    [TestCase(1000, 1, 100, 1)]
    public void EvaluateStarCount_SingleStar_ReturnsOne(
        int score, int expectedStarCount, int maxScoreThreshold, int maxStarCount)
    {
        SetScore(score);

        int starCount = _ratingEvaluator.EvaluateStarCount(maxScoreThreshold, maxStarCount);

        Assert.AreEqual(expectedStarCount, starCount);
    }

    [TestCase(0, 1, 200, 4)]
    [TestCase(49, 1, 200, 4)]
    [TestCase(50, 1, 200, 4)]
    [TestCase(100, 2, 200, 4)]
    [TestCase(150, 3, 200, 4)]
    [TestCase(200, 4, 200, 4)]
    [TestCase(500, 4, 200, 4)]
    public void EvaluateStarCount_FourStars_ReturnsExpectedCount(
        int score, int expectedStarCount, int maxScoreThreshold, int maxStarCount)
    {
        SetScore(score);

        int starCount = _ratingEvaluator.EvaluateStarCount(maxScoreThreshold, maxStarCount);

        Assert.AreEqual(expectedStarCount, starCount);
    }

    [Test]
    public void EvaluateStarCount_EmitsResultOnEvaluated()
    {
        SetScore(200);
        int? emittedStarCount = null;

        using IDisposable subscription = _ratingEvaluator.Evaluated
            .Subscribe(count => emittedStarCount = count);

        int starCount = _ratingEvaluator.EvaluateStarCount(ThreeStarMaxScore, ThreeStarCount);

        Assert.AreEqual(starCount, emittedStarCount);
        Assert.AreEqual(2, starCount);
    }

    private void SetScore(int score)
    {
        if (score > 0)
            _scoreModel.Increase(score);
    }
}
