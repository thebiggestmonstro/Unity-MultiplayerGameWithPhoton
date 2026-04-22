using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UI_ChooseColor : MonoBehaviour
{
    private GameObject panel;
    private PhotonView cachedPhotonView;
    private GameObject playerNameBG;

    private void Start()
    {
        Cursor.visible = true;
        panel = Util.FindParent(gameObject, "UI_PanelChooseColor", false);
        playerNameBG = UIManager.GetNameBGUI("UI_ImgPlayerNameBG").gameObject;
        cachedPhotonView = gameObject.GetOrAddComponent<PhotonView>();
    }

    public void SelectButton(int buttonNumber)
    {
        int localViewID = GetLocalPlayerViewID();

        if (localViewID == -1)
        {
            Debug.LogError("[UI_ChooseColor] Local player not found.");
            return;
        }

        cachedPhotonView.RPC("SelectedColor", RpcTarget.AllBuffered, buttonNumber, localViewID);
        Cursor.visible = false;
        panel.SetActive(false);
    }

    private int GetLocalPlayerViewID()
    {
        foreach (var playerController in GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None))
        {
            GameObject player = playerController.gameObject;
            var pv = player.GetOrAddComponent<PhotonView>();
            if (pv != null && pv.IsMine)
            {
                return pv.ViewID;
            }
        }

        return -1;
    }

    [PunRPC]
    void SelectedColor(int buttonNumber, int ownerViewID)
    {
        foreach (var playerController in GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None))
        {
            playerController.GetOrAddComponent<DisplayColor>()?.ApplyColor(buttonNumber, ownerViewID);
        }

        if (playerNameBG == null)
        {
            playerNameBG = UIManager.GetNameBGUI("UI_ImgPlayerNameBG").gameObject;
        }

        playerNameBG.GetOrAddComponent<UI_Timer>().BeginTimer();
        gameObject.SetActive(false);
    }
}
