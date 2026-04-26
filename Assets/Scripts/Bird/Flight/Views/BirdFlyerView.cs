using R3;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _collided = new(false);

    private Rigidbody _rigidbody = null;

    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();

            return _rigidbody;
        }
    }

    public ReadOnlyReactiveProperty<bool> Collided => _collided;

    private void OnCollisionEnter(Collision collision) => _collided.Value = true;
}
