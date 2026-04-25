using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
public class BirdEntityView : MonoBehaviour
{
    private BirdFlyerView _flyerView = null;

    public BirdFlyerView FlyerView
    {
        get
        {
            if (_flyerView == null)
                _flyerView = GetComponent<BirdFlyerView>();

            return _flyerView;
        }
    }
}
