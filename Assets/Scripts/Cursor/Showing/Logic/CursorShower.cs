using UnityEngine;

public class CursorShower
{
    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetShowing(bool isShowing)
    {
        if (isShowing)
            Show();
        else
            Hide();
    }
}
