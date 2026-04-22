using Photon.Pun;
using System.Collections;
using UnityEngine;

public class DisplayColor : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Color32[] colors;
    private UI_NickName nickNameUI;
    private UI_PlayerNameBG playerNameBGUI;
    private PhotonView cachedPhotonView;
    private Renderer playerRenderer;

    private void Awake()
    {
        cachedPhotonView = gameObject.GetOrAddComponent<PhotonView>();
        playerRenderer = transform.GetChild(1).gameObject.GetOrAddComponent<Renderer>();
    }

    private void Start()
    {
        nickNameUI = UIManager.GetNickNameUI("UI_ImgPlayerNameBG");
        playerNameBGUI = UIManager.GetNameBGUI("UI_ImgPlayerNameBG");
    }

    public void ApplyColor(int colorIndex, int ownerViewID)
    {
        if (cachedPhotonView.ViewID != ownerViewID)
        {
            return;
        }

        if (nickNameUI == null)
        {
            nickNameUI = UIManager.GetNickNameUI("UI_ImgPlayerNameBG");
        }

        playerRenderer.material.color = colors[colorIndex];
        nickNameUI.names[colorIndex].gameObject.SetActive(true);
        nickNameUI.healthbars[colorIndex].gameObject.SetActive(true);
        nickNameUI.names[colorIndex].text = cachedPhotonView.Owner.NickName;
    }

    public void RemoveData()
    {
        GetComponent<PhotonView>().RPC("RemoveMe", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void RemoveMe()
    {
        for (int i = 0; i < playerNameBGUI.gameObject.GetComponent<UI_NickName>().names.Length; i++)
        {
            if (GetComponent<PhotonView>().Owner.NickName == playerNameBGUI.GetComponent<UI_NickName>().names[i].text)
            {
                playerNameBGUI.GetComponent<UI_NickName>().names[i].gameObject.SetActive(false);
                playerNameBGUI.GetComponent<UI_NickName>().healthbars[i].gameObject.SetActive(false);
            }
        }
    }

    public void RoomExit()
    {
        StartCoroutine(GetReadyToLeave());
    }

    IEnumerator GetReadyToLeave()
    {
        yield return new WaitForSeconds(1);
        playerNameBGUI.GetComponent<UI_NickName>().Leaving();
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }
}