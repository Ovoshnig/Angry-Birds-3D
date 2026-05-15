using R3;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _isEnabled = new(false);

    public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

    protected virtual void OnEnable() => _isEnabled.Value = true;

    protected virtual void OnDisable() => _isEnabled.Value = false;
}
