using UnityEngine;

public class MainCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

        UnityEngine.Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
