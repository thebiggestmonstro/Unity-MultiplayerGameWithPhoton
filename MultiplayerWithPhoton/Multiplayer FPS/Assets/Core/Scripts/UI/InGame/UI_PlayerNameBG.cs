using UnityEngine;

public class UI_PlayerNameBG : MonoBehaviour
{
    private void Awake()
    {
        UIManager.ResgisterNameBGUI(gameObject.name, this);
    }
}
