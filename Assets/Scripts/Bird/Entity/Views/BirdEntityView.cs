using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
[RequireComponent(typeof(ObjectColliderView))]
public class BirdEntityView : MonoBehaviour
{
    private BirdFlyerView _flyerView = null;
    private ObjectColliderView _colliderView = null;

    public BirdFlyerView FlyerView
    {
        get
        {
            if (_flyerView == null)
                _flyerView = GetComponent<BirdFlyerView>();

            return _flyerView;
        }
    }

    public ObjectColliderView ColliderView
    {
        get
        {
            if (_colliderView == null)
                _colliderView = GetComponent<ObjectColliderView>();

            return _colliderView;
        }
    }
}
