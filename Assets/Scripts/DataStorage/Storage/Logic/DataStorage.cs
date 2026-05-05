using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using UnityEngine;
using VContainer.Unity;

public abstract class DataStorage : IAsyncStartable, IDisposable
{
    private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    private readonly Dictionary<string, object> _defaultDataStore = new();
    private readonly Subject<Unit> _resetHappened = new();

    private Dictionary<string, JsonElement> _dataStore = new();

    public Observable<Unit> ResetHappened => _resetHappened;

    protected abstract string FileName { get; }

    protected string FilePath => Path.Combine(Application.persistentDataPath, FileName);

    public async UniTask StartAsync(CancellationToken token) => await LoadDataAsync();

    public void Dispose()
    {
        SaveDataAsync().Forget();
        _resetHappened.Dispose();
    }

    public virtual T Get<T>(string key, T defaultValue)
    {
        _defaultDataStore[key] = defaultValue;

        if (!_dataStore.TryGetValue(key, out JsonElement storedValue))
            return defaultValue;

        try
        {
            return storedValue.Deserialize<T>(_jsonOptions);
        }
        catch (Exception exeption)
        {
            Debug.LogWarning($"Failed to deserialize key {key}: {exeption.Message}");
            return defaultValue;
        }
    }

    public virtual void Set<T>(string key, T value) =>
        _dataStore[key] = JsonSerializer.SerializeToElement(value, _jsonOptions);

    public virtual void ResetData()
    {
        _dataStore.Clear();

        foreach (var kvp in _defaultDataStore)
            _dataStore[kvp.Key] = JsonSerializer.SerializeToElement(kvp.Value, _jsonOptions);

        _resetHappened.OnNext(Unit.Default);
    }

    protected virtual async UniTask LoadDataAsync()
    {
        if (!File.Exists(FilePath))
            return;

        using FileStream stream = new(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        _dataStore = await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(stream, _jsonOptions) ?? new();
    }

    protected virtual async UniTask SaveDataAsync()
    {
        using FileStream stream = new(FilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await JsonSerializer.SerializeAsync(stream, _dataStore, _jsonOptions);
    }
}
