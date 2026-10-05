using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;
using UnityScreen = UnityEngine.Screen;

namespace Ovoshnig.Screen.ResolutionAdjustment
{
    public class ResolutionAdjuster : IStartable, IDisposable
    {
        private readonly ReactiveProperty<int> _currentResolutionIndex = new(0);

        public List<ResolutionData> Resolutions { get; private set; }
        public ReadOnlyReactiveProperty<int> CurrentResolutionIndex => _currentResolutionIndex;

        public void Start()
        {
            ResolutionData currentResolution = GetCurrentResolutionData();

            Resolutions = UnityScreen.resolutions
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
            UnityScreen.SetResolution(resolution.Width, resolution.Height, UnityScreen.fullScreenMode, resolution.RefreshRate);
            _currentResolutionIndex.Value = index;
        }

        private ResolutionData GetCurrentResolutionData()
        {
            int width = UnityScreen.fullScreen ? UnityScreen.currentResolution.width : UnityScreen.width;
            int height = UnityScreen.fullScreen ? UnityScreen.currentResolution.height : UnityScreen.height;
            RefreshRate refreshRate = UnityScreen.currentResolution.refreshRateRatio;

            return new ResolutionData(width, height, refreshRate);
        }
    }
}
