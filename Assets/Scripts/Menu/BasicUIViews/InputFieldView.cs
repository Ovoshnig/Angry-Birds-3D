using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public abstract class InputFieldView : MonoBehaviour
{
    private TMP_InputField _inputField;

    public Observable<string> ValueChanged { get; private set; }

    protected virtual void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        ValueChanged = _inputField.onValueChanged.AsObservable(destroyCancellationToken);
    }
}
