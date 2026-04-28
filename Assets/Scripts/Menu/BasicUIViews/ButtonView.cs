using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : MonoBehaviour
{
    private Button _button = null;

    public Observable<Unit> Clicked => Button.OnClickAsObservable();

    private Button Button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<Button>();

            return _button;
        }
    }

    public void SetEnable(bool value) => enabled = value;

    public void SetInteractable(bool value) => Button.interactable = value;
}
