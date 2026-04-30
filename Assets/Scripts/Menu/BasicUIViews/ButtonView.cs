using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : MonoBehaviour
{
    private Button _button;

    public Observable<Unit> Clicked { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        Clicked = _button.OnClickAsObservable();
    }

    public void SetInteractable(bool value) => _button.interactable = value;
}
