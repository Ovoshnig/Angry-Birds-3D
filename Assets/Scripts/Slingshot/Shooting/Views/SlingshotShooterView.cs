using UnityEngine;
using UnityEngine.Audio;

public class SlingshotShooterView : MonoBehaviour
{
    [field: SerializeField] public Transform LeftAnchor { get; private set; }
    [field: SerializeField] public Transform RightAnchor { get; private set; }
    [field: SerializeField] public Transform CenterAnchor { get; private set; }
    [field: SerializeField] public LineRenderer LeftRubber { get; private set; }
    [field: SerializeField] public LineRenderer RightRubber { get; private set; }
    [field: SerializeField] public AudioClip DraggingResource { get; private set; }
    [field: SerializeField] public AudioResource ShotResource { get; private set; }
}
