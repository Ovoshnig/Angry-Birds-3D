using NUnit.Framework;
using R3;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public sealed class LevelStateTrackerTests
{
    private TrackerHarness _harness;

    [SetUp]
    public void SetUp() => _harness = TrackerHarness.Create();

    [TearDown]
    public void TearDown() => _harness.Dispose();

    [Test]
    public void PostStart_EmitsOnStarted()
    {
        EventCounter started = _harness.Subscribe(_harness.Tracker.Started);

        _harness.Tracker.PostStart();

        Assert.That(started.Count, Is.EqualTo(1));
    }

    [Test]
    public void MovedToNext_WhenBirdDestroyed_PigsRemain_AndBirdsInQueue_Emits()
    {
        _harness.SetPigCount(2);
        _harness.ReplaceBirdQueue(birdsInQueue: 1);
        EventCounter movedToNext = _harness.Subscribe(_harness.Tracker.MovedToNext);

        _harness.EmitBirdDestroyed();

        Assert.That(movedToNext.Count, Is.EqualTo(1));
    }

    [Test]
    public void MovedToNext_WhenBirdDestroyed_PigsRemain_AndBirdOnSlingshot_Emits()
    {
        _harness.SetPigCount(1);
        _harness.SetCurrentBird(_harness.CreateRigidbody());
        EventCounter movedToNext = _harness.Subscribe(_harness.Tracker.MovedToNext);

        _harness.EmitBirdDestroyed();

        Assert.That(movedToNext.Count, Is.EqualTo(1));
    }

    [Test]
    public void MovedToNext_WhenBirdDestroyed_AndNoPigsLeft_DoesNotEmit()
    {
        _harness.SetPigCount(0);
        _harness.ReplaceBirdQueue(birdsInQueue: 1);
        EventCounter movedToNext = _harness.Subscribe(_harness.Tracker.MovedToNext);

        _harness.EmitBirdDestroyed();

        Assert.That(movedToNext.Count, Is.EqualTo(0));
    }

    [Test]
    public void Cleared_WhenBirdDestroyed_AndNoPigsLeft_Emits()
    {
        _harness.SetPigCount(0);
        EventCounter cleared = _harness.Subscribe(_harness.Tracker.Cleared);

        _harness.EmitBirdDestroyed();

        Assert.That(cleared.Count, Is.EqualTo(1));
    }

    [Test]
    public void Cleared_WhenPigsLeft_AndBirdOnSlingshot_Emits()
    {
        _harness.SetPigCount(1);
        _harness.SetCurrentBird(_harness.CreateRigidbody());
        EventCounter cleared = _harness.Subscribe(_harness.Tracker.Cleared);

        _harness.SetPigCount(0);

        Assert.That(cleared.Count, Is.EqualTo(1));
    }

    [Test]
    public void Cleared_EmitsOnlyOnce_OnSubsequentBirdDestructions()
    {
        _harness.SetPigCount(0);
        EventCounter cleared = _harness.Subscribe(_harness.Tracker.Cleared);

        _harness.EmitBirdDestroyed();
        _harness.EmitBirdDestroyed();

        Assert.That(cleared.Count, Is.EqualTo(1));
    }

    [Test]
    public void Failed_WhenBirdDestroyed_NoBirdsNoSlingshot_PigsRemain_Emits()
    {
        _harness.SetPigCount(3);
        EventCounter failed = _harness.Subscribe(_harness.Tracker.Failed);

        _harness.EmitBirdDestroyed();

        Assert.That(failed.Count, Is.EqualTo(1));
    }

    [Test]
    public void Failed_EmitsOnlyOnce_OnSubsequentBirdDestructions()
    {
        _harness.SetPigCount(2);
        EventCounter failed = _harness.Subscribe(_harness.Tracker.Failed);

        _harness.EmitBirdDestroyed();
        _harness.EmitBirdDestroyed();

        Assert.That(failed.Count, Is.EqualTo(1));
    }

    [Test]
    public void Completed_WhenLevelCleared_Emits()
    {
        _harness.SetPigCount(0);
        EventCounter completed = _harness.Subscribe(_harness.Tracker.Completed);

        _harness.EmitBirdDestroyed();

        Assert.That(completed.Count, Is.EqualTo(1));
    }

    [Test]
    public void Completed_WhenLevelFailed_Emits()
    {
        _harness.SetPigCount(1);
        EventCounter completed = _harness.Subscribe(_harness.Tracker.Completed);

        _harness.EmitBirdDestroyed();

        Assert.That(completed.Count, Is.EqualTo(1));
    }

    [Test]
    public void Completed_EmitsOnlyOnce_WhenBothClearedAndFailedCouldApply()
    {
        _harness.SetPigCount(0);
        EventCounter completed = _harness.Subscribe(_harness.Tracker.Completed);

        _harness.EmitBirdDestroyed();
        _harness.SetPigCount(2);
        _harness.EmitBirdDestroyed();

        Assert.That(completed.Count, Is.EqualTo(1));
    }

    private sealed class EventCounter
    {
        public int Count { get; private set; }

        public void OnEvent() => Count++;
    }

    private sealed class TrackerHarness : IDisposable
    {
        private readonly List<IDisposable> _subscriptions = new();
        private readonly List<GameObject> _gameObjects = new();
        private readonly List<IDisposable> _disposables = new();

        public LevelStateTracker Tracker { get; private set; }
        public BirdDestroyer BirdDestroyer { get; private set; }
        public BirdQueue BirdQueue { get; private set; }
        public PigTracker PigTracker { get; private set; }
        public SlingshotShooter SlingshotShooter { get; private set; }

        public static TrackerHarness Create() => new();

        private TrackerHarness()
        {
            ObjectCollider objectCollider = new(Array.Empty<CollidableEntityView>(), new CollisionSettings());
            BirdDestroyer = new BirdDestroyer(objectCollider, new BirdSettings());
            BirdQueue = new BirdQueue(Array.Empty<BirdEntityView>());
            ObjectDestroyer objectDestroyer = new(objectCollider);
            PigTracker = new PigTracker(Array.Empty<PigEntityView>(), objectDestroyer);
            SlingshotShooter = new SlingshotShooter(null, null, null, null);
            Tracker = new LevelStateTracker(BirdQueue, BirdDestroyer, PigTracker, SlingshotShooter);

            _disposables.Add(BirdDestroyer);
            _disposables.Add(PigTracker);
            _disposables.Add(SlingshotShooter);
            _disposables.Add(objectDestroyer);
        }

        public EventCounter Subscribe(Observable<Unit> observable)
        {
            EventCounter counter = new();
            _subscriptions.Add(observable.Subscribe(_ => counter.OnEvent()));
            return counter;
        }

        public void EmitBirdDestroyed() =>
            GetSubject<BirdEntityView>(BirdDestroyer, "_destroyed").OnNext(null);

        public void SetPigCount(int count) =>
            GetReactiveProperty(PigTracker, "_pigCount").Value = count;

        public void SetCurrentBird(Rigidbody rigidbody) =>
            typeof(SlingshotShooter)
                .GetField("_currentBird", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(SlingshotShooter, rigidbody);

        public Rigidbody CreateRigidbody()
        {
            GameObject gameObject = new("TestBird");
            _gameObjects.Add(gameObject);
            return gameObject.AddComponent<Rigidbody>();
        }

        public void ReplaceBirdQueue(int birdsInQueue)
        {
            BirdEntityView[] birds = new BirdEntityView[birdsInQueue];

            for (int i = 0; i < birdsInQueue; i++)
            {
                GameObject gameObject = new($"QueuedBird_{i}");
                _gameObjects.Add(gameObject);
                birds[i] = gameObject.AddComponent<BirdEntityView>();
            }

            BirdQueue = new BirdQueue(birds);
            Tracker?.Dispose();
            Tracker = new LevelStateTracker(BirdQueue, BirdDestroyer, PigTracker, SlingshotShooter);
        }

        public void Dispose()
        {
            foreach (IDisposable subscription in _subscriptions)
                subscription.Dispose();

            Tracker.Dispose();

            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();

            foreach (GameObject gameObject in _gameObjects)
                if (gameObject != null)
                    UnityEngine.Object.DestroyImmediate(gameObject);
        }

        private static Subject<T> GetSubject<T>(object instance, string fieldName) =>
            (Subject<T>)instance.GetType()
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(instance);

        private static ReactiveProperty<int> GetReactiveProperty(PigTracker tracker, string fieldName) =>
            (ReactiveProperty<int>)typeof(PigTracker)
                .GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(tracker);
    }
}
