using R3;
using UnityEngine.UIElements;

namespace Ovoshnig.Extensions.UIToolkit;

public static class VisualElementExtensions
{
    public static Observable<Unit> ClickedAsObservable(this Button button) =>
        Observable.FromEvent(
            handler => button.clicked += handler,
            handler => button.clicked -= handler);

    public static Observable<T> ValueChangedAsObservable<T>(this INotifyValueChanged<T> control) =>
        Observable.FromEvent<EventCallback<ChangeEvent<T>>, T>(
            handler => evt => handler(evt.newValue),
            handler => control.RegisterValueChangedCallback(handler),
            handler => control.UnregisterValueChangedCallback(handler));
}
