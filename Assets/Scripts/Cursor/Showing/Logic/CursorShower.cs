using UnityEngine;
using UnityCursor = UnityEngine.Cursor;

namespace Ovoshnig.Cursor.Showing;

public static class CursorShower
{
    public static void Show()
    {
        UnityCursor.lockState = CursorLockMode.None;
        UnityCursor.visible = true;
    }

    public static void Hide()
    {
        UnityCursor.lockState = CursorLockMode.Locked;
        UnityCursor.visible = false;
    }

    public static void SetShowing(bool isShowing)
    {
        if (isShowing)
            Show();
        else
            Hide();
    }
}
