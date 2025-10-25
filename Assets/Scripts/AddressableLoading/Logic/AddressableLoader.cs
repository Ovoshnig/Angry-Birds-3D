using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

public class AddressableLoader : IDisposable
{
    private record AddressableData(AsyncOperationHandle Handle, object Assets);

    private readonly Dictionary<string, AddressableData> _assetsByLabels = new();

    public async UniTask<IList<T>> LoadAssets<T>(IEnumerable<string> labels, CancellationToken token) 
        where T : Object
    {
        string key = GetKeyFromLabels(labels);

        if (_assetsByLabels.TryGetValue(key, out AddressableData cachedData))
            return (IList<T>)cachedData.Assets;

        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(
            labels,
            null,
            Addressables.MergeMode.Intersection);

        await handle.ToUniTask(cancellationToken: token);

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _assetsByLabels[key] = new AddressableData(handle, handle.Result);
            return handle.Result;
        }

        Debug.LogWarning($"Failed to load assets for labels: {key}");
        handle.Release();
        return new List<T>();
    }

    public void ReleaseAssets(IEnumerable<string> labels)
    {
        string key = GetKeyFromLabels(labels);

        if (_assetsByLabels.TryGetValue(key, out AddressableData data))
        {
            data.Handle.Release();
            _assetsByLabels.Remove(key);
        }
    }

    private string GetKeyFromLabels(IEnumerable<string> labels)
    {
        SortedSet<string> sortedSet = new(labels);
        return string.Join(",", sortedSet);
    }

    public void Dispose()
    {
        foreach (var data in _assetsByLabels.Values)
            data.Handle.Release();

        _assetsByLabels.Clear();
    }
}
