using Photon.Pun;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UI_ChooseColor : MonoBehaviour
{
    private GameObject panel;
    private PhotonView cachedPhotonView;

    private void Start()
    {
        Cursor.visible = true;
        panel = Util.FindParent(gameObject, "Panel_ChooseColor", false);
        cachedPhotonView = GetComponent<PhotonView>();
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
            var pv = player.GetComponent<PhotonView>();
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
            player.GetComponent<DisplayColor>()?.ApplyColor(buttonNumber, ownerViewID);
        }

        gameObject.SetActive(false);
    }
}
