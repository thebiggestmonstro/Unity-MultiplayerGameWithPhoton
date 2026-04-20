using UnityEngine;

public class UI_PlayerAim : MonoBehaviour
{
    private void Awake()
    {
        UIManager.RegisterAimUI(gameObject.name, this);
    }
}
