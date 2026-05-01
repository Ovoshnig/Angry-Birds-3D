using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

public class ResolutionAdjuster : IInitializable, IDisposable
{
    private readonly ReactiveProperty<int> _currentResolutionIndex = new(0);

    public List<ResolutionData> Resolutions { get; private set; }
    public ReadOnlyReactiveProperty<int> CurrentResolutionIndex => _currentResolutionIndex;

    public void Initialize()
    {
        ResolutionData currentResolution = GetCurrentResolutionData();

        Resolutions = Screen.resolutions
            .Select(r => new ResolutionData(r.width, r.height, r.refreshRateRatio))
            .Distinct()
            .ToList();

        if (!Resolutions.Contains(currentResolution))
            Resolutions.Add(currentResolution);

        Resolutions.Sort();
        _currentResolutionIndex.Value = Resolutions.IndexOf(currentResolution);
    }

    public void Dispose() => _currentResolutionIndex.Dispose();

    public void SetResolution(int index)
    {
        if (index < 0 || index >= Resolutions.Count)
        {
            Debug.LogError($"Resolution with index {index} not found.");
            return;
        }

        ResolutionData resolution = Resolutions[index];
        Screen.SetResolution(resolution.Width, resolution.Height, Screen.fullScreenMode, resolution.RefreshRate);
        _currentResolutionIndex.Value = index;
    }

    private ResolutionData GetCurrentResolutionData()
    {
        int width = Screen.fullScreen ? Screen.currentResolution.width : Screen.width;
        int height = Screen.fullScreen ? Screen.currentResolution.height : Screen.height;
        RefreshRate refreshRate = Screen.currentResolution.refreshRateRatio;

        return new ResolutionData(width, height, refreshRate);
    }
}
