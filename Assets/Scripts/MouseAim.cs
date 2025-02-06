using UnityEngine;

public class MouseAim : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
