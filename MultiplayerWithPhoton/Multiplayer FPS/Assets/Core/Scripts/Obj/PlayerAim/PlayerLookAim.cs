using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAim : MonoBehaviour
{
    [SerializeField]
    private GameObject crossHair;
    private Vector3 worldPosition;
    private Vector3 screenPosition;

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
