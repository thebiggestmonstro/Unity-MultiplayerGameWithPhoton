using Photon.Pun;
using UnityEngine;

public class PlayerLookAimRef : MonoBehaviour
{
    private UI_PlayerAim playerAimUI;
    private PhotonView photonView;
    public bool isDead = false;

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

        if (photonView.IsMine && isDead == false)
        {
            transform.position = playerAimUI.transform.position;
        }
    }
}
