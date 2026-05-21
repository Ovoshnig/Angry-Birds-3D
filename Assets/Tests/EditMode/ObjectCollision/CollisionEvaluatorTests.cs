using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CollisionEvaluatorTests
{
    private const float GlidingThreshold = 0.5f;
    private const float CollisionThreshold = 10f;
    private const float DamageThreshold = 100f;

    private CollisionEvaluator _evaluator;

    [SetUp]
    public void SetUp()
    {
        CollisionSettings settings = new(GlidingThreshold, CollisionThreshold, DamageThreshold);
        _evaluator = new CollisionEvaluator(settings);
    }

    [Test]
    public void TryEvaluate_NoContacts_ReturnsFalse_AndZeroImpactForce()
    {
        CollisionData data = CreateCollisionData(contactCount: 0, impulseMagnitude: 50f);

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out float impactForce);

        Assert.IsFalse(result);
        Assert.AreEqual(CollisionType.Collision, type);
        Assert.AreEqual(0f, impactForce);
    }

    [Test]
    public void TryEvaluate_ImpactForceAtDamageThreshold_ReturnsDamage()
    {
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(DamageThreshold));

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out float impactForce);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Damage, type);
        Assert.AreEqual(DamageThreshold, impactForce, 1e-4f);
    }

    [Test]
    public void TryEvaluate_ImpactForceAboveDamageThreshold_ReturnsDamage()
    {
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(DamageThreshold + 50f));

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out _);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Damage, type);
    }

    [Test]
    public void TryEvaluate_ImpactForceBetweenCollisionAndDamage_ReturnsCollision()
    {
        float impactForce = (CollisionThreshold + DamageThreshold) * 0.5f;
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(impactForce));

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out float actualImpactForce);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Collision, type);
        Assert.AreEqual(impactForce, actualImpactForce, 1e-4f);
    }

    [Test]
    public void TryEvaluate_ImpactForceAtCollisionThreshold_ReturnsCollision()
    {
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(CollisionThreshold));

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out _);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Collision, type);
    }

    [Test]
    public void TryEvaluate_BelowCollisionThresholdButGliding_ReturnsGliding()
    {
        float impactForce = CollisionThreshold * 0.5f;
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(impactForce),
            contactNormal: Vector3.up,
            relativeVelocity: Vector3.right);

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out _);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Gliding, type);
    }

    [Test]
    public void TryEvaluate_BelowCollisionThresholdNotGliding_ReturnsFalse()
    {
        float impactForce = CollisionThreshold * 0.5f;
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(impactForce),
            contactNormal: Vector3.up,
            relativeVelocity: Vector3.up);

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out float actualImpactForce);

        Assert.IsFalse(result);
        Assert.AreEqual(CollisionType.Collision, type);
        Assert.AreEqual(impactForce, actualImpactForce, 1e-4f);
    }

    [Test]
    public void TryEvaluate_HighImpactForceWithGlidingAngle_ReturnsDamageNotGliding()
    {
        CollisionData data = CreateCollisionData(impulseMagnitude: ImpulseForImpactForce(DamageThreshold + 1f),
            contactNormal: Vector3.up,
            relativeVelocity: Vector3.right);

        bool result = _evaluator.TryEvaluate(data, out CollisionType type, out _);

        Assert.IsTrue(result);
        Assert.AreEqual(CollisionType.Damage, type);
    }

    private static float ImpulseForImpactForce(float impactForce) =>
        impactForce * Time.fixedDeltaTime;

    private static CollisionData CreateCollisionData(float impulseMagnitude,
        int contactCount = 1,
        Vector3? contactNormal = null,
        Vector3? relativeVelocity = null) =>
        new(contactNormal ?? Vector3.up,
            relativeVelocity ?? Vector3.forward,
            impulseMagnitude,
            contactCount);
}
