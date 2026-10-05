using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class AudioTuningMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<AudioMixerTunerGamePauserMediator>();
    }
}
