using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public sealed class MusicQueueTests
{
    private MusicQueue _queue = null;

    [SetUp]
    public void SetUp() => _queue = new MusicQueue();

    [Test]
    public void Clear_OnNewQueue_DoesNotThrow_AndQueueRemainsEmpty()
    {
        Assert.DoesNotThrow(_queue.Clear);
        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void SetClipKeys_Null_Throws() => Assert.Throws<ArgumentNullException>(() => _queue.SetClipKeys(null));

    [Test]
    public void SetClipKeys_EmptySequence_MakesQueueEmpty()
    {
        _queue.SetClipKeys(Array.Empty<object>());

        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void TryGetNextClipKey_WhenEmpty_ReturnsFalse_AndOutputsNull()
    {
        bool ok = _queue.TryGetNextClipKey(out object key);

        Assert.False(ok);
        Assert.Null(key);
    }

    [Test]
    public void TryGetNextClipKey_AfterSetClipKeys_ReturnsItemsInOriginalOrder()
    {
        object a = new();
        object b = new();
        object c = new();

        _queue.SetClipKeys(new object[] { a, b, c });

        Assert.True(_queue.TryGetNextClipKey(out object k1));
        Assert.AreSame(a, k1);
        Assert.True(_queue.TryGetNextClipKey(out object k2));
        Assert.AreSame(b, k2);
        Assert.True(_queue.TryGetNextClipKey(out object k3));
        Assert.AreSame(c, k3);
        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void Clear_AfterSetClipKeys_EmptiesQueue()
    {
        _queue.SetClipKeys(new object[] { new(), new() });

        _queue.Clear();

        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void ShuffleClipKeys_WhenEmpty_DoesNotThrow_AndRemainsEmpty()
    {
        Assert.DoesNotThrow(_queue.ShuffleClipKeys);
        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void ShuffleClipKeys_WhenSingleElement_PreservesThatElement()
    {
        object only = new();
        _queue.SetClipKeys(new object[] { only });

        _queue.ShuffleClipKeys();

        Assert.True(_queue.TryGetNextClipKey(out object key));
        Assert.AreSame(only, key);
        Assert.False(_queue.TryGetNextClipKey(out _));
    }

    [Test]
    public void ShuffleClipKeys_PreservesAllElements_AndCount_ForUniqueElements()
    {
        object[] items = Enumerable.Range(0, 50).Select(_ => new object()).ToArray();
        _queue.SetClipKeys(items);

        _queue.ShuffleClipKeys();

        object[] dequeued = DequeueAll(_queue).ToArray();

        Assert.That(dequeued.Length, Is.EqualTo(items.Length));
        CollectionAssert.AreEquivalent(items, dequeued);
    }

    [Test]
    public void ShuffleClipKeys_PreservesDuplicates_MultisetEquality()
    {
        object shared = new();
        object[] items = new object[] { shared, new(), shared, shared, new(), shared };
        _queue.SetClipKeys(items);

        _queue.ShuffleClipKeys();

        object[] dequeued = DequeueAll(_queue).ToArray();

        Assert.That(dequeued.Length, Is.EqualTo(items.Length));
        AssertMultisetEqualByReference(items, dequeued);
    }

    [Test]
    public void ShuffleClipKeys_DoesNotLoseElements_AfterMultipleShuffles()
    {
        object[] items = Enumerable.Range(0, 30).Select(_ => new object()).ToArray();
        _queue.SetClipKeys(items);

        for (int i = 0; i < 10; i++)
            _queue.ShuffleClipKeys();

        object[] dequeued = DequeueAll(_queue).ToArray();
        Assert.That(dequeued.Length, Is.EqualTo(items.Length));
        CollectionAssert.AreEquivalent(items, dequeued);
    }

    private static IEnumerable<object> DequeueAll(MusicQueue queue)
    {
        while (queue.TryGetNextClipKey(out object key))
            yield return key;
    }

    private static void AssertMultisetEqualByReference(IReadOnlyList<object> expected, IReadOnlyList<object> actual)
    {
        static Dictionary<object, int> CountByRef(IEnumerable<object> seq)
        {
            Dictionary<object, int> dict = new(ReferenceEqualityComparer.Instance);
            foreach (object obj in seq)
            {
                dict.TryGetValue(obj, out int count);
                dict[obj] = count + 1;
            }

            return dict;
        }

        Dictionary<object, int> e = CountByRef(expected);
        Dictionary<object, int> a = CountByRef(actual);

        Assert.That(a.Count, Is.EqualTo(e.Count));

        foreach (KeyValuePair<object, int> kv in e)
        {
            object key = kv.Key;
            int expectedCount = kv.Value;
            Assert.True(a.TryGetValue(key, out int actualCount));
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }

    private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();

        public new bool Equals(object x, object y) => ReferenceEquals(x, y);

        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }
}
