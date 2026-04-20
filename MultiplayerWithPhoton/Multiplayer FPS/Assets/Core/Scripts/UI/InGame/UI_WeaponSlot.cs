using UnityEngine;

public class UI_WeaponSlot : MonoBehaviour
{
    private void Awake()
    {
        UIManager.RegisterSlotUI(gameObject.name, this);
    }
}
