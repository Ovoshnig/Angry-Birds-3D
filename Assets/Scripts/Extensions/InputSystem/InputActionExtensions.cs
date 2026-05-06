using R3;
using UnityEngine.InputSystem;

public static class InputActionExtensions
{
    public static ReadOnlyReactiveProperty<bool> ToButtonProperty(this InputAction action)
    {
        return Observable.Create<bool>(observer =>
        {
            void OnAction(InputAction.CallbackContext context) => observer.OnNext(context.ReadValueAsButton());

            action.performed += OnAction;
            action.canceled += OnAction;

            return Disposable.Create(() =>
            {
                action.performed -= OnAction;
                action.canceled -= OnAction;
            });
        }).ToReadOnlyReactiveProperty(action.IsPressed());
    }

    public static ReadOnlyReactiveProperty<T> ToValueProperty<T>(this InputAction action) where T : struct
    {
        return Observable.Create<T>(observer =>
        {
            void OnAction(InputAction.CallbackContext context) => observer.OnNext(context.ReadValue<T>());

            action.performed += OnAction;
            action.canceled += OnAction;

            return Disposable.Create(() =>
            {
                action.performed -= OnAction;
                action.canceled -= OnAction;
            });
        }).ToReadOnlyReactiveProperty(action.ReadValue<T>());
    }
}
