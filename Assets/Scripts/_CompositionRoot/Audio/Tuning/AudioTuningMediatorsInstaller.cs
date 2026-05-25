using VContainer;
using VContainer.Unity;

public class AudioTuningMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<AudioMixerTunerGamePauserMediator>();
}
