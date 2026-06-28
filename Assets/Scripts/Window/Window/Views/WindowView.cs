using R3;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    private ReactiveProperty<bool> _isActive = null;

    public ReadOnlyReactiveProperty<bool> IsActive
    {
        get
        {
            if (_isActive == null)
            {
                _isActive = new ReactiveProperty<bool>();

                Observable
                    .EveryValueChanged(gameObject, g => g.activeSelf)
                    .Subscribe(activeSelf => _isActive.Value = activeSelf)
                    .AddTo(gameObject);
            }

            return _isActive;
        }
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
