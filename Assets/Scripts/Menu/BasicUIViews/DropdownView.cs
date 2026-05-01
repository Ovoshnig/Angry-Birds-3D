using R3;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public abstract class DropdownView : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    public Observable<int> ValueChanged { get; private set; }

    protected virtual void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        ValueChanged = _dropdown.onValueChanged.AsObservable(destroyCancellationToken);
    }

    public void SetOptions(List<TMP_Dropdown.OptionData> options) => _dropdown.options = options;

    public void SetValue(int value) => _dropdown.value = value;

    public void SetValueWithoutNotify(int value) => _dropdown.SetValueWithoutNotify(value);
}
