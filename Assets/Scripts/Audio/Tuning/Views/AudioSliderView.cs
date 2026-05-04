using UnityEngine;

public class AudioSliderView : SliderView
{
    [field: SerializeField] public AudioChannel Channel { get; private set; }
}
