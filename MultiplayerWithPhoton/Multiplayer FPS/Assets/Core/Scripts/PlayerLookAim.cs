using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAim : MonoBehaviour
{
    [SerializeField]
    private GameObject crossHair;
    private Vector3 worldPosition;
    private Vector3 screenPosition;

    private void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            screenPosition = new Vector3(mousePos.x, mousePos.y, 3f);

            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            transform.position = worldPosition;

            crossHair.transform.position = mousePos;
        }
    }
}
