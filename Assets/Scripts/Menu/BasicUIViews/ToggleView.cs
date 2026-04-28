using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class ToggleView : MonoBehaviour
{
    private Toggle _toggle = null;

    public Observable<bool> ValueChanged => Toggle.OnValueChangedAsObservable();

    private Toggle Toggle
    {
        get
        {
            if (_toggle == null)
                _toggle = GetComponent<Toggle>();

            return _toggle;
        }
    }

    public void SetIsOn(bool value) => Toggle.isOn = value;

    public void SetIsOnWithoutNotify(bool value) => Toggle.SetIsOnWithoutNotify(value);
}
