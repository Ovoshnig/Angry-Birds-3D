using NUnit.Framework;
using System;

[TestFixture]
public class ScoreModelTests
{
    private ScoreModel _scoreModel;

    [SetUp]
    public void SetUp() => _scoreModel = new ScoreModel();

    [TearDown]
    public void TearDown() => _scoreModel.Dispose();

    [Test]
    public void InitialScore_WhenCreated_IsZero() => Assert.AreEqual(0, _scoreModel.Score.CurrentValue);

    [TestCase(1)]
    [TestCase(15)]
    [TestCase(100)]
    [TestCase(9999)]
    public void Increase_PositiveValue_AddsToScore(int positiveValue)
    {
        int initialScore = _scoreModel.Score.CurrentValue;

        _scoreModel.Increase(positiveValue);

        Assert.AreEqual(initialScore + positiveValue, _scoreModel.Score.CurrentValue);
    }

    [TestCase(-1)]
    [TestCase(-100)]
    [TestCase(-9999)]
    public void Increase_NegativeValue_ThrowsArgumentException(int negativeValue) =>
        Assert.Throws<ArgumentException>(() => _scoreModel.Increase(negativeValue));

    [Test]
    public void Reset_AfterScoreIncreased_SetsScoreBackToZero()
    {
        _scoreModel.Increase(100);

        _scoreModel.Reset();

        Assert.AreEqual(0, _scoreModel.Score.CurrentValue);
    }
}
