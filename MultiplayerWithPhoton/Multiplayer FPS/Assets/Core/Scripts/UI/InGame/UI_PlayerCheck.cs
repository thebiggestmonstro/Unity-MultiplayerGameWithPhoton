using Photon.Pun;
using TMPro;
using UnityEngine;

public class UI_PlayerCheck : MonoBehaviour
{
    [SerializeField] 
    int maxPlayersInRoom = 2;
    [SerializeField] 
    TextMeshProUGUI currentPlayers;

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            this.gameObject.SetActive(false);
        }

        currentPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + maxPlayersInRoom.ToString();
    }

    /*
     * // 새로운 플레이어가 방에 들어왔을 때 실행
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCountUI();
        CheckRoomFull();
    }

    // 플레이어가 방에서 나갔을 때 실행
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCountUI();
    }

    private void UpdatePlayerCountUI()
    {
        if (PhotonNetwork.CurrentRoom == null || currentPlayersText == null) return;

        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        currentPlayersText.text = $"{currentCount} / {maxPlayersInRoom}";
    }

    private void CheckRoomFull()
    {
        if (PhotonNetwork.CurrentRoom == null) return;

        // 마스터 클라이언트(방장)만 방의 상태를 변경할 권한을 갖는 것이 안전합니다.
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayersInRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            // UI를 끄는 로직은 필요에 따라 유지하거나, 방이 찼다는 표시로 변경하세요.
            this.gameObject.SetActive(false);
        }
    }
     */
}
