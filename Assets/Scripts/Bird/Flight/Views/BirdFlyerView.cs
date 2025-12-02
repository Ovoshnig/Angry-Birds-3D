using R3;
using UnityEngine;

public class BirdFlyerView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _wasCollided = new(false);

    public ReadOnlyReactiveProperty<bool> Collided => _wasCollided;

    private void OnCollisionEnter(Collision collision) => _wasCollided.Value = true;
}
