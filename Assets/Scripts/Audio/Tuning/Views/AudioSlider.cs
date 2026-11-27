using UnityEngine.UIElements;

namespace Ovoshnig.Audio.Tuning;

[UxmlElement]
public partial class AudioSlider : Slider
{
    [UxmlAttribute]
    public AudioChannel Channel { get; private set; }
}
