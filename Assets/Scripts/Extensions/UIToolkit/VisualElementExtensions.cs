using R3;
using UnityEngine.UIElements;

namespace Ovoshnig.Extensions.UIToolkit
{
    public static class VisualElementExtensions
    {
        public static Observable<Unit> ClickedAsObservable(this Button button) =>
            Observable.FromEvent(
                handler => button.clicked += handler,
                handler => button.clicked -= handler);
    }
}
