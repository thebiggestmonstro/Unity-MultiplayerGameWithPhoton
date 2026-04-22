using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_NickName : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] names;
    public Image[] healthbars;
    private GameObject playerCheckUIObject;

    private void Awake()
    {
        UIManager.RegisterNickNameUI(gameObject.name, this);
    }

    private void Start()
    {
        for (int i = 0; i < names.Length; i++)
        {
            names[i].gameObject.SetActive(false);
            healthbars[i].gameObject.SetActive(false);
        }

        playerCheckUIObject = UIManager.GetPlayerCheckUI("UI_PlayerWaiting").gameObject;
    }

    public void Leaving()
    {
        StartCoroutine("BackToLobby");
    }

    IEnumerator BackToLobby()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void ReturnToLobby()
    {
        playerCheckUIObject.SetActive(false);
        RoomExit();
    }

    void RoomExit()
    {
        StartCoroutine(ToLobby());
    }

    IEnumerator ToLobby()
    {
        yield return new WaitForSeconds(0.4f);
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
