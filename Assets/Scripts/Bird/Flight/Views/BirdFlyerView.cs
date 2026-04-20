using R3;
using UnityEngine;
using UnityEngine.Audio;

public class BirdFlyerView : MonoBehaviour
{
    [field: SerializeField] public AudioResource SelectionResource { get; private set; }
    [field: SerializeField] public AudioResource FlyingResource { get; private set; }

    private readonly ReactiveProperty<bool> _wasCollided = new(false);

    public ReadOnlyReactiveProperty<bool> Collided => _wasCollided;

    private void OnCollisionEnter(Collision collision) => _wasCollided.Value = true;
}
