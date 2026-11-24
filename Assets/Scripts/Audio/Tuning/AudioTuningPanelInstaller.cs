using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Audio.Tuning
{
    [Serializable]
    public class AudioTuningPanelInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<AudioSliderPanelView>();
            builder.RegisterEntryPoint<AudioSliderModelsPanelMediator>();
        }
    }
}
