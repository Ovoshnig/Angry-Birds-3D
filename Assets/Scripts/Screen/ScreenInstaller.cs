using Ovoshnig.Screen.FullScreenAdjustment;
using Ovoshnig.Screen.ResolutionAdjustment;
using Ovoshnig.Screen.VSyncAdjustment;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Screen
{
    [Serializable]
    public class ScreenInstaller : IInstaller
    {
        [SerializeField] private FullScreenAdjustmentInstaller _fullScreenAdjustmentInstaller;
        [SerializeField] private ResolutionAdjustmentInstaller _resolutionAdjustmentInstaller;
        [SerializeField] private VSyncAdjustmentInstaller _vSyncAdjustmentInstaller;

        public void Install(IContainerBuilder builder)
        {
            _fullScreenAdjustmentInstaller.Install(builder);
            _resolutionAdjustmentInstaller.Install(builder);
            _vSyncAdjustmentInstaller.Install(builder);
        }
    }
}
