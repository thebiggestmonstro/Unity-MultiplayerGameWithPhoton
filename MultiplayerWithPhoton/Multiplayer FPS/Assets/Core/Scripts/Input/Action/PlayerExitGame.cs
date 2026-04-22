using Photon.Pun;
using UnityEngine;

public class PlayerExitGame : MonoBehaviour
{
    private DisplayColor displayColor;
    private GameObject playerCheck;

    private void Start()
    {
        displayColor = gameObject.GetOrAddComponent<DisplayColor>();
        playerCheck = UIManager.GetPlayerCheckUI("UI_PlayerWaiting").gameObject;
    }

    public void HandleExitGame()
    {
        if (gameObject.GetOrAddComponent<PhotonView>().IsMine == true && playerCheck.activeInHierarchy == false)
        {
            displayColor.RemoveData();
            displayColor.RoomExit();
        }
    }
}
