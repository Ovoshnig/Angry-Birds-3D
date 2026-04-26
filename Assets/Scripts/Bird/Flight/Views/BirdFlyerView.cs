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
}
