using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonView : UIView
{
    private readonly ReactiveProperty<bool> _isInteractable = new();

    private Button _button;

    public Observable<Unit> Clicked { get; private set; }
    public ReadOnlyReactiveProperty<bool> IsInteractable => _isInteractable;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        Clicked = _button.OnClickAsObservable();
        _isInteractable.Value = _button.IsInteractable();
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
        _isInteractable.Value = value;
    }
}
