using UnityEngine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 13 Dec 2022 by @Nuutti J.
/// </summary>

public class CustomCursor : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [SerializeField] Texture2D aimingReticle;
    static Vector2 customCursorSize;
    static Texture2D customCursorTex;

    void Awake() {
        customCursorSize.x = aimingReticle.width;
        customCursorSize.y = aimingReticle.height;
        customCursorTex = aimingReticle;
    }

    void Start()
    {
        ConfineMouse();
        SetCustomCursor();
    }

    public static void SetDefaultCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    
    public static void SetCustomCursor()
    {
        Vector2 hotspot = new Vector2(customCursorSize.x / 2, customCursorSize.y / 2);
        Cursor.SetCursor(customCursorTex, hotspot, CursorMode.Auto);
    }

    //Added by Daniel K. - 01122022
    private void ConfineMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
