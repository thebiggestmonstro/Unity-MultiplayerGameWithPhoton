using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField playerNickname;
    private string setName = "";
    [SerializeField]
    private GameObject serverConnecting;

    private void Start()
    {
        serverConnecting.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server!!!");
        SceneManager.LoadScene("Lobby");
    }

    public void UpdateText()
    {
        setName = playerNickname.text;
        PhotonNetwork.LocalPlayer.NickName = setName;
    }

    public void OnClickEnterButton()
    {
        if (setName != "")
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            serverConnecting.SetActive(true);
        }
    }

    public void OnClickExitButton()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
