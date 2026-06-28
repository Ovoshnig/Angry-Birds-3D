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
                    .EveryValueChanged(gameObject, g => g != null && g.activeSelf)
                    .Subscribe(activeSelf => _isActive.Value = activeSelf)
                    .RegisterTo(destroyCancellationToken);
            }

            return _isActive;
        }
    }

    private void OnDestroy() => _isActive.Dispose();

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
