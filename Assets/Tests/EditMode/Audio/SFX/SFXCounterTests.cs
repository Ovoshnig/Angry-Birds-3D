using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class SFXCounterTests
{
    private SFXCounter _sfxCounter;
    private AudioResource _resourceA;
    private AudioResource _resourceB;

    [SetUp]
    public void SetUp()
    {
        _sfxCounter = new();
        _resourceA = AudioClip.Create("TestClipA", 1, 1, 1000, false);
        _resourceB = AudioClip.Create("TestClipB", 1, 1, 1000, false);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_resourceA);
        Object.DestroyImmediate(_resourceB);
    }

    [Test]
    public void GetCount_WhenResourceNotAdded_ReturnsZero() =>
        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(0));

    [Test]
    public void Increment_Once_SetsCountToOne()
    {
        _sfxCounter.Increment(_resourceA);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(1));
    }

    [Test]
    public void Increment_MultipleTimes_IncreasesCountCorrectly()
    {
        _sfxCounter.Increment(_resourceA);
        _sfxCounter.Increment(_resourceA);
        _sfxCounter.Increment(_resourceA);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(3));
    }

    [Test]
    public void Increment_DifferentResources_TracksCountsIndependently()
    {
        _sfxCounter.Increment(_resourceA);
        _sfxCounter.Increment(_resourceA);
        _sfxCounter.Increment(_resourceB);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(2));
        Assert.That(_sfxCounter.GetCount(_resourceB), Is.EqualTo(1));
    }

    [Test]
    public void Decrement_WhenCountIsGreaterThanOne_DecreasesCountByOne()
    {
        _sfxCounter.Increment(_resourceA);
        _sfxCounter.Increment(_resourceA);

        _sfxCounter.Decrement(_resourceA);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(1));
    }

    [Test]
    public void Decrement_WhenCountIsOne_RemovesResourceAndReturnsZero()
    {
        _sfxCounter.Increment(_resourceA);

        _sfxCounter.Decrement(_resourceA);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(0));
    }

    [Test]
    public void Decrement_WhenResourceDoesNotExist_DoesNotThrowAndReturnsZero()
    {
        _sfxCounter.Decrement(_resourceA);

        Assert.That(_sfxCounter.GetCount(_resourceA), Is.EqualTo(0));
    }
}
