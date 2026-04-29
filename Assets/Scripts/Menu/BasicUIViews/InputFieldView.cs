using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public abstract class InputFieldView : MonoBehaviour
{
    private TMP_InputField _inputField;

    public Observable<string> ValueChanged => _inputField.onValueChanged.AsObservable(destroyCancellationToken);

    private void Awake() => _inputField = GetComponent<TMP_InputField>();
}
