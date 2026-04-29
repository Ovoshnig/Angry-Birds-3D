using R3;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public abstract class DropdownView : MonoBehaviour
{
    private TMP_Dropdown _dropdown = null;

    public Observable<int> ValueChanged => Dropdown.onValueChanged.AsObservable(destroyCancellationToken);

    private TMP_Dropdown Dropdown
    {
        get
        {
            if (_dropdown == null)
                _dropdown = GetComponent<TMP_Dropdown>();

            return _dropdown;
        }
    }

    public void SetOptions(List<TMP_Dropdown.OptionData> options) =>
        Dropdown.options = options;

    public void SetValue(int value) => Dropdown.value = value;

    public void SetValueWithoutNotify(int value) => Dropdown.SetValueWithoutNotify(value);
}
