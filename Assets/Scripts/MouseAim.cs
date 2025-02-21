using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField]private Camera mainCamera;

    void Start()
    {
        // Cursor.visible = false; // Subir com deploy para esconder mouse
       // mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
