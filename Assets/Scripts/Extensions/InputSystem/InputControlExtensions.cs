using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Ovoshnig.Extensions.InputSystem;

public static class InputControlExtensions
{
    extension(InputControl inputControl)
    {
        public string ToCaseIndependentString()
        {
            if (inputControl is KeyControl keyControl)
            {
                Key key = keyControl.keyCode;

                if (key is >= Key.Backquote and <= Key.Z)
                {
                    InputAction tempAction = new(binding: $"<Keyboard>/{key}");
                    return tempAction.bindings[0].ToDisplayString();
                }
                else
                {
                    return inputControl.displayName;
                }
            }

            return inputControl.displayName;
        }
    }
}
