using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject connectingText;
    private string levelName = "";

    private TypedLobby currentLobby;
    TypedLobby killCount = new TypedLobby("killCount", LobbyType.Default);
    TypedLobby teamBattle = new TypedLobby("teamBattle", LobbyType.Default);
    TypedLobby noRespawn = new TypedLobby("noRespawn", LobbyType.Default);

    private void Start()
    {
        connectingText.SetActive(false);
    }

    public void BackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public void JoinGameKillCount()
    {
        levelName = "Floor layout";
        currentLobby = killCount;
        PhotonNetwork.JoinLobby(killCount);
    }

    public void JoinGameTeamBattle()
    {
        levelName = "Floor layout";
        currentLobby = teamBattle;
        PhotonNetwork.JoinLobby(teamBattle);
    }

    public void JoinGameNoRespawn()
    {
        levelName = "Floor layout";
        currentLobby = noRespawn;
        PhotonNetwork.JoinLobby(noRespawn);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joined random room failed, creating a new room");

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 6
        };

        PhotonNetwork.CreateRoom("Arena" + Random.Range(1, 1000), roomOptions, currentLobby);
    }

    public override void OnJoinedRoom()
    {
        connectingText.SetActive(true);
        PhotonNetwork.LoadLevel(levelName);
    }
}
