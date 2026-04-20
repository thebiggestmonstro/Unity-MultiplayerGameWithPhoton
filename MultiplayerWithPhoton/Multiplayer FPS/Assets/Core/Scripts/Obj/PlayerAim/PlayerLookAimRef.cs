using Photon.Pun;
using UnityEngine;

public class PlayerLookAimRef : MonoBehaviour
{
    private UI_PlayerAim playerAimUI;
    private PhotonView photonView;

    void Start()
    {
        playerAimUI = UIManager.GetAimUI("UI_PlayerAim");
        photonView = Util.FindParent<PhotonView>(this.gameObject, null, false);
    }

    void FixedUpdate()
    {
        if (playerAimUI == null)
        {
            playerAimUI = UIManager.GetAimUI("UI_PlayerAim");
        }

        if (photonView.IsMine)
        {
            transform.position = playerAimUI.transform.position;
        }
    }
}
