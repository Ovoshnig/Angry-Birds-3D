using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class ToggleView : MonoBehaviour
{
    private Toggle _toggle;

    public Observable<bool> ValueChanged { get; private set; }

    protected virtual void Awake()
    {
        _toggle = GetComponent<Toggle>();
        ValueChanged = _toggle.OnValueChangedAsObservable();
    }

    public void SetIsOn(bool value) => _toggle.isOn = value;

    public void SetIsOnWithoutNotify(bool value) => _toggle.SetIsOnWithoutNotify(value);
}
