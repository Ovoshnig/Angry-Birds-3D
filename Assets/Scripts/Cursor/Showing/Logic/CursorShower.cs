using UnityEngine;
using UnityCursor = UnityEngine.Cursor;

namespace Ovoshnig.Cursor.Showing
{
    public class CursorShower
    {
        public void Show()
        {
            UnityCursor.lockState = CursorLockMode.None;
            UnityCursor.visible = true;
        }

        public void Hide()
        {
            UnityCursor.lockState = CursorLockMode.Locked;
            UnityCursor.visible = false;
        }

        public void SetShowing(bool isShowing)
        {
            if (isShowing)
                Show();
            else
                Hide();
        }
    }
}
