using R3;
using UnityEngine;

public class UIView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _isEnabled = new(false);

    public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

    private void OnEnable() => _isEnabled.Value = true;

    private void OnDisable() => _isEnabled.Value = false;
}
