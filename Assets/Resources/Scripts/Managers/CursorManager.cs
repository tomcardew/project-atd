using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D handCursor;
    public Texture2D grabCursor;
    public Texture2D selectCursor;

    private void Awake()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetHandCursor()
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetGrabCursor()
    {
        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetSelectCursor()
    {
        Cursor.SetCursor(selectCursor, Vector2.zero, CursorMode.Auto);
    }
}
