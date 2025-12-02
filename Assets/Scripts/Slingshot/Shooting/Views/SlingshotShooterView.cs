using UnityEngine;

public class SlingshotShooterView : MonoBehaviour
{
    [field: SerializeField] public Transform LeftAnchor { get; private set; }
    [field: SerializeField] public Transform RightAnchor { get; private set; }
    [field: SerializeField] public Transform CenterAnchor { get; private set; }
    [field: SerializeField] public LineRenderer LeftRubber { get; private set; }
    [field: SerializeField] public LineRenderer RightRubber { get; private set; }
}
