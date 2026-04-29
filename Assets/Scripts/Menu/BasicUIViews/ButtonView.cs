using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : MonoBehaviour
{
    private Button _button;

    public Observable<Unit> Clicked => _button.OnClickAsObservable();

    private void Awake() => _button = GetComponent<Button>();

    public void SetEnable(bool value) => enabled = value;

    public void SetInteractable(bool value) => _button.interactable = value;
}
