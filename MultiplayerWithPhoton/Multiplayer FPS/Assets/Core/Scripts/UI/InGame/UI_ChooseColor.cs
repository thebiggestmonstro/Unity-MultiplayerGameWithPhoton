using Photon.Pun;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UI_ChooseColor : MonoBehaviour
{
    private GameObject panel;
    private PhotonView cachedPhotonView;
    private GameObject playerNameBG;

    private void Start()
    {
        Cursor.visible = true;
        panel = Util.FindParent(gameObject, "Panel_ChooseColor", false);
        playerNameBG = GameObject.FindGameObjectWithTag("PlayerNameBG");
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
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
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
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetOrAddComponent<DisplayColor>()?.ApplyColor(buttonNumber, ownerViewID);
        }

        playerNameBG.GetOrAddComponent<UI_Timer>().BeginTimer();
        gameObject.SetActive(false);
    }
}
