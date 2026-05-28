using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class RecordRatingSaver : IStartable, IDisposable
{
    private readonly SaveStorage _saveStorage;
    private readonly RatingEvaluator _ratingEvaluator;
    private readonly ReactiveProperty<int> _record = new();
    private readonly CompositeDisposable _disposables = new();

    public RecordRatingSaver(SaveStorage saveStorage, RatingEvaluator ratingEvaluator)
    {
        _saveStorage = saveStorage;
        _ratingEvaluator = ratingEvaluator;
    }

    public ReadOnlyReactiveProperty<int> Record => _record;

    public void Start()
    {
        _ratingEvaluator.Evaluated
            .Subscribe(OnRatingEvaluated)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _record.Dispose();
    }

    private void OnRatingEvaluated(int starCount)
    {
        Dictionary<int, int> starRecordBylevelIndex = _saveStorage
            .Get(SaveConstants.StarRecordBylevelIndex, new Dictionary<int, int>());

        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (starRecordBylevelIndex.TryGetValue(currentLevel, out int starRecord))
            starRecord = Mathf.Max(starCount, starRecord);
        else
            starRecord = starCount;

        starRecordBylevelIndex[currentLevel] = starRecord;
        _saveStorage.Set(SaveConstants.StarRecordBylevelIndex, starRecordBylevelIndex);

        _record.Value = starRecord;
    }
}
