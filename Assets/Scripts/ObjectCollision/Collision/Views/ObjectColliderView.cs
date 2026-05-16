using R3;
using R3.Triggers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObjectColliderView : MonoBehaviour
{
    public Observable<Collision> Collided { get; private set; }

    private void Awake() => Collided = gameObject.OnCollisionEnterAsObservable();
}
