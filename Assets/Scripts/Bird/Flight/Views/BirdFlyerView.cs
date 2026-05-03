using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdFlyerView : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private void Awake() => Rigidbody = GetComponent<Rigidbody>();

    public void LookAtVelocityDirection()
    {
        if (Rigidbody.linearVelocity.sqrMagnitude != 0f)
            Rigidbody.transform.forward = Rigidbody.linearVelocity.normalized;
    }
}
