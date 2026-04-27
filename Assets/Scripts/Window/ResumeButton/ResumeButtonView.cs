using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeButtonView : MonoBehaviour
{
    private readonly Subject<Unit> _clicked = new();

    private Button _button;

    public Observable<Unit> Clicked => _clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.OnClickAsObservable()
            .Subscribe(_clicked.OnNext)
            .AddTo(this);
    }

    private void OnDestroy() => _clicked.Dispose();
}
