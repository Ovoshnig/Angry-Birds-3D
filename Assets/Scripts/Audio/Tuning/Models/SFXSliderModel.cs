using Ovoshnig.DataStorage.Storage;
using Ovoshnig.GameSettings;

namespace Ovoshnig.Audio.Tuning
{
    public sealed class SFXSliderModel : AudioSliderModel
    {
        public SFXSliderModel(SettingsStorage settingsStorage, AudioSettings audioSettings)
            : base(settingsStorage, audioSettings)
        {
        }

        public override AudioChannel Channel => AudioChannel.SFX;
        public override string MixerParameterName => AudioMixerConstants.SFXVolumeParameter;

        protected override string DataKey => SettingsConstants.SFXVolumeKey;
    }
}
