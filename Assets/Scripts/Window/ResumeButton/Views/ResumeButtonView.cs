using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeButtonView : UIView
{
    private Button _button;

    public Observable<Unit> Clicked { get; private set; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        Clicked = _button.OnClickAsObservable();
    }
}
