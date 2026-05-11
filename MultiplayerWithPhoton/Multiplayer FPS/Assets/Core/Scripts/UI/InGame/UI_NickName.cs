using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_NickName : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] names;
    public Image[] healthbars;
    private GameObject playerCheckUIObject;
    [SerializeField]
    private GameObject displayPanel;
    [SerializeField]
    TextMeshProUGUI messageText;
    public int[] killScore;
    public bool teamMode = false;

    private PhotonView pv;
    private Coroutine messageCoroutine;

    private void Awake()
    {
        UIManager.RegisterNickNameUI(gameObject.name, this);
        pv = gameObject.GetComponent<PhotonView>();
    }

    private void Start()
    {
        displayPanel.SetActive(false);

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

    public void RunMessage(string winnerName, string loserName)
    {
        pv.RPC("DisplayMessage", RpcTarget.All, winnerName, loserName);
        UpdateKillScore(winnerName);
    }

        [PunRPC]
    void DisplayMessage(string winnerName, string loserName)
    {
        displayPanel.SetActive(true);
        messageText.text = winnerName + " killed " + loserName;

        if (PhotonNetwork.IsMasterClient)
        {
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
            }

            messageCoroutine = StartCoroutine(TurnOffMessage());
        }
    }

    IEnumerator TurnOffMessage()
    {
        yield return new WaitForSeconds(3);
        messageCoroutine = null;
        pv.RPC("MessageOff", RpcTarget.All);
    }

    [PunRPC]
    void MessageOff()
    {
        displayPanel.SetActive(false);
        messageCoroutine = null;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient && displayPanel.activeSelf)
        {
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
            }

            messageCoroutine = StartCoroutine(TurnOffMessage());
        }
    }

    void UpdateKillScore(string winnerName)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (winnerName == names[i].text)
            {
                killScore[i]++;
            }
        }
    }
}
