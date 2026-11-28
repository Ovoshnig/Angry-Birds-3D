using Ovoshnig.DataStorage.Storage;
using Ovoshnig.GameSettings;

namespace Ovoshnig.Audio.Tuning;

public sealed class MusicSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
    : AudioSliderModel(settingsStorage, audioSettings)
{
    public override AudioChannel Channel => AudioChannel.Music;
    public override string MixerParameterName => AudioMixerConstants.MusicVolumeParameter;

    protected override string DataKey => SettingsConstants.MusicVolumeKey;
}
