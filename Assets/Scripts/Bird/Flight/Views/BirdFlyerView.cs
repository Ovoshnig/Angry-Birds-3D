using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
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

    public void LookAtVelocityDirection()
    {
        if (_rigidbody.linearVelocity.sqrMagnitude != 0f)
            _rigidbody.transform.forward = _rigidbody.linearVelocity.normalized;
    }
}
