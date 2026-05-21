using NUnit.Framework;
using System;
using UnityEngine;

[TestFixture]
public class HealthModelTests
{
    private HealthModel _healthModel;

    [SetUp]
    public void Setup() => _healthModel = new HealthModel(1000f);

    [TestCase(2f)]
    [TestCase(50f)]
    [TestCase(3000f)]
    public void ApplyDamage_PositiveAmount_ReducesHealth(float positiveAmount)
    {
        float initialHealth = _healthModel.Health;

        _healthModel.ApplyDamage(positiveAmount);

        float expectedhealth = Mathf.Max(0f, initialHealth - positiveAmount);
        Assert.AreEqual(expectedhealth, _healthModel.Health);
    }

    [Test]
    public void ApplyDamage_ZeroAmount_HealthDoesntChange()
    {
        float initialHealth = _healthModel.Health;

        _healthModel.ApplyDamage(0f);

        Assert.AreEqual(initialHealth, _healthModel.Health);
    }

    [TestCase(-2f)]
    [TestCase(-50f)]
    [TestCase(-3000f)]
    public void ApplyDamage_NegativeAmount_ThrowsArgumentException(float negativeAmount) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => _healthModel.ApplyDamage(negativeAmount));
}
