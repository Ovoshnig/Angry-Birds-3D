using Ovoshnig.UI.Basic;
using UnityEngine;

namespace Ovoshnig.Audio.Tuning
{
    public class AudioSliderView : SliderView
    {
        [field: SerializeField] public AudioChannel Channel { get; private set; }
    }
}
