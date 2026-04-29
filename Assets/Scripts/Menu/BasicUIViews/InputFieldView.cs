using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public abstract class InputFieldView : MonoBehaviour
{
    private TMP_InputField _inputField = null;

    public Observable<string> ValueChanged => InputField.onValueChanged.AsObservable(destroyCancellationToken);

    private TMP_InputField InputField
    {
        get
        {
            if (_inputField == null)
                _inputField = GetComponent<TMP_InputField>();

            return _inputField;
        }
    }
}
