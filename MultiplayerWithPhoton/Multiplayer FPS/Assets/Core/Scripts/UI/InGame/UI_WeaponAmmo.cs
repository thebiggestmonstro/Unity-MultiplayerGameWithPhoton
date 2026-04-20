using UnityEngine;

public class UI_WeaponAmmo : MonoBehaviour
{
    private void Awake()
    {
        UIManager.RegisterAmmoUI(gameObject.name, this);
    }
}
