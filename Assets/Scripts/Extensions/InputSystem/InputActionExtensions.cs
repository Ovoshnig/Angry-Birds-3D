using R3;
using UnityEngine.InputSystem;

namespace Ovoshnig.Extensions.InputSystem;

public static class InputActionExtensions
{
    extension(InputAction action)
    {
        public ReadOnlyReactiveProperty<bool> ToButtonProperty() =>
            Observable.Create<bool>(observer =>
            {
                void OnAction(InputAction.CallbackContext context) => observer.OnNext(
                    context.ReadValueAsButton());

                action.performed += OnAction;
                action.canceled += OnAction;

                return Disposable.Create(() =>
                {
                    action.performed -= OnAction;
                    action.canceled -= OnAction;
                });
            })
            .ToReadOnlyReactiveProperty(action.IsPressed());

        public ReadOnlyReactiveProperty<T> ToValueProperty<T>() where T : struct =>
            Observable.Create<T>(observer =>
            {
                void OnAction(InputAction.CallbackContext context) => observer.OnNext(context.ReadValue<T>());

                action.performed += OnAction;
                action.canceled += OnAction;

                return Disposable.Create(() =>
                {
                    action.performed -= OnAction;
                    action.canceled -= OnAction;
                });
            })
            .ToReadOnlyReactiveProperty(action.ReadValue<T>());
    }
}
