using R3;
using UnityEngine.UIElements;

namespace Ovoshnig.Extensions.UIToolkit;

public static class VisualElementExtensions
{
    extension(Button button)
    {
        public Observable<Unit> ClickedAsObservable() =>
            Observable.FromEvent(
                handler => button.clicked += handler,
                handler => button.clicked -= handler);
    }

    extension<T>(INotifyValueChanged<T> control)
    {
        public Observable<T> ValueChangedAsObservable() =>
            Observable.FromEvent<EventCallback<ChangeEvent<T>>, T>(
                handler => evt => handler(evt.newValue),
                handler => control.RegisterValueChangedCallback(handler),
                handler => control.UnregisterValueChangedCallback(handler));
    }
}
