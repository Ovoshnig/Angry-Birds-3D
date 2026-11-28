using Cysharp.Threading.Tasks;
using Ovoshnig.Audio.Music.SceneMusicMapping;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Ovoshnig.Audio.Music.ClipLoading;

public class AddressablesClipLoader : IClipLoader
{
    private AsyncOperationHandle<AudioClip> _handle;

    public async UniTask<Dictionary<MusicCategory, IEnumerable<object>>> LoadClipKeysAsync(CancellationToken token)
    {
        Dictionary<MusicCategory, IEnumerable<object>> musicClipKeys = [];

        foreach (MusicCategory category in Enum.GetValues<MusicCategory>())
        {
            string categoryName = category.ToString();
            categoryName = Regex.Replace(categoryName, "(?<!^)([A-Z])", " $1").ToLower();
            List<string> labels = ["audio", "music", categoryName];

            AsyncOperationHandle<IList<IResourceLocation>> locationsHandle = Addressables.LoadResourceLocationsAsync(
                labels,
                Addressables.MergeMode.Intersection);
            await locationsHandle.ToUniTask(cancellationToken: token);
            IList<IResourceLocation> locations = locationsHandle.Result;
            musicClipKeys[category] = locations;

            locationsHandle.Release();
        }

        return musicClipKeys;
    }

    public async UniTask<AudioClip> LoadClipAsync(object address, CancellationToken token)
    {
        IResourceLocation resourceLocation = (IResourceLocation)address;
        _handle = Addressables.LoadAssetAsync<AudioClip>(resourceLocation);
        await _handle.ToUniTask(cancellationToken: token);

        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            AudioClip clip = _handle.Result;
            return clip;
        }

        return null;
    }

    public void UnloadClip(AudioClip clip)
    {
        if (_handle.IsValid())
            _handle.Release();
    }
}
