using Photon.Pun;
using TMPro;
using UnityEngine;

public class UI_PlayerCheck : MonoBehaviour
{
    [SerializeField] 
    int maxPlayersInRoom = 2;
    [SerializeField]
    GameObject howToPlay;
    [SerializeField]
    GameObject joinedCount;
    [SerializeField]
    GameObject enterGameButton;

    private TextMeshProUGUI countText;

    private void Awake()
    {
        UIManager.ResgisterPlayerCheckUI(gameObject.name, this);
    }

    private void Start()
    {
        countText = joinedCount.GetOrAddComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
        {
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.IsOpen)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }

            howToPlay.gameObject.SetActive(false);
            joinedCount.gameObject.SetActive(false);
            enterGameButton.gameObject.SetActive(true);
        }

        if (!enterGameButton.activeInHierarchy)
        {
            countText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + maxPlayersInRoom.ToString();
        }
        else 
        {
            countText.text = "";
        }
    }

    public void EnterTheArena()
    {
        gameObject.SetActive(false);
    }
}
