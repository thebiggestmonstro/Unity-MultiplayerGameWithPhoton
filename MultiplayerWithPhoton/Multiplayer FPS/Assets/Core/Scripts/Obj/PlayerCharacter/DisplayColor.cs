using Photon.Pun;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayColor : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Color32[] colors;
    [SerializeField]
    private Color32[] teamColors;
    [SerializeField]
    AudioClip[] gunShotSounds;
    private UI_NickName nickNameUI;
    private UI_PlayerNameBG playerNameBGUI;
    private PhotonView pv;
    private Renderer playerRenderer;
    public bool teamMode = false;

    private void Awake()
    {
        pv = gameObject.GetOrAddComponent<PhotonView>();
        playerRenderer = transform.GetChild(1).gameObject.GetOrAddComponent<Renderer>();
    }

    private void Start()
    {
        nickNameUI = UIManager.GetNickNameUI("UI_ImgPlayerNameBG");
        playerNameBGUI = UIManager.GetNameBGUI("UI_ImgPlayerNameBG");
        InvokeRepeating("CheckTime", 1, 1);
        teamMode = nickNameUI.GetComponent<UI_NickName>().teamMode;
    }

    private void Update()
    {
        if (GetComponent<Animator>().GetBool("Hit"))
        {
            StartCoroutine(Recover());   
        }
    }

    public void ApplyColor(int colorIndex, int ownerViewID)
    {
        if (pv.ViewID != ownerViewID)
        {
            return;
        }

        if (nickNameUI == null)
        {
            nickNameUI = UIManager.GetNickNameUI("UI_ImgPlayerNameBG");
        }

        if (teamMode == false)
        {
            playerRenderer.material.color = colors[colorIndex];
            nickNameUI.names[colorIndex].gameObject.SetActive(true);
            nickNameUI.healthbars[colorIndex].gameObject.SetActive(true);
            nickNameUI.names[colorIndex].text = pv.Owner.NickName;
        }
        else if (teamMode)
        {
            playerRenderer.material.color = teamColors[colorIndex];
            nickNameUI.names[colorIndex].gameObject.SetActive(true);
            nickNameUI.healthbars[colorIndex].gameObject.SetActive(true);
            nickNameUI.names[colorIndex].text = pv.Owner.NickName;
        }
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
            if (pv.Owner.NickName == playerNameBGUI.GetComponent<UI_NickName>().names[i].text)
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

    public void PlayGunShot(string name, int weaponNumber)
    {
        pv.RPC("PlaySound", RpcTarget.All, name,weaponNumber);
    }

    [PunRPC]
    void PlaySound(string name, int weaponNumber)
    {
        for (int i = 0; i < playerNameBGUI.GetComponent<UI_NickName>().names.Length; i++)
        {
            if (name == playerNameBGUI.GetOrAddComponent<UI_NickName>().names[i].text)
            {
                gameObject.GetOrAddComponent<AudioSource>().clip = gunShotSounds[weaponNumber];
                gameObject.GetOrAddComponent<AudioSource>().Play();
            }
        }
    }

    public void DeliverDamage(string shooterName, string targetName, float damageAmt)
    {
        pv.RPC("TakeDamage", RpcTarget.AllBuffered, shooterName, targetName, damageAmt);
    }

    [PunRPC]
    void TakeDamage(string shooterName, string targetName, float damageAmt)
    {
        for (int i = 0; i < playerNameBGUI.GetComponent<UI_NickName>().names.Length; i++)
        {
            if (targetName == playerNameBGUI.GetComponent<UI_NickName>().names[i].text)
            {
                if (playerNameBGUI.GetComponent<UI_NickName>().healthbars[i].gameObject.GetComponent<Image>().fillAmount > 0.1f)
                {
                    GetComponent<Animator>().SetBool("Hit", true);
                    playerNameBGUI.GetComponent<UI_NickName>().healthbars[i].gameObject.GetComponent<Image>().fillAmount -= (damageAmt / 100);
                }
                else
                {
                    playerNameBGUI.GetComponent<UI_NickName>().healthbars[i].gameObject.GetComponent<Image>().fillAmount = 0;
                    GetComponent<Animator>().SetBool("Dead", true);
                    gameObject.GetComponent<PlayerMovement>().isDead =true;
                    gameObject.GetComponent<PlayerFire>().isDead = true;
                    gameObject.GetComponent<PlayerWeaponChange>().isDead = true;
                    gameObject.GetComponentInChildren<PlayerLookAimRef>().isDead = true;
                    playerNameBGUI.GetComponent<UI_NickName>().RunMessage(shooterName, targetName);
                    gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
            }
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(0.03f);
        GetComponent<Animator>().SetBool("Hit", false);
    }

    public void Respawn(string playerName)
    {
        pv.RPC("ResetForReplay",RpcTarget.AllBuffered, playerName);
    }

    [PunRPC]
    void ResetForReplay(string playerName)
    {
        for (int i = 0; i < playerNameBGUI.GetComponent<UI_NickName>().names.Length; i++)
        {
            if (playerName == playerNameBGUI.GetComponent<UI_NickName>().names[i].text)
            {
                GetComponent<Animator>().SetBool("Dead", false);
                gameObject.GetComponent<PlayerWeaponChange>().isDead= false;
                gameObject.GetComponent<PlayerMovement>().isDead = false;
                gameObject.GetComponent<PlayerFire>().isDead = false;
                gameObject.GetComponentInChildren<PlayerLookAimRef>().isDead = false;
                gameObject.layer = LayerMask.NameToLayer("Default");
                playerNameBGUI.GetComponent<UI_NickName>().healthbars[i].gameObject.GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    void CheckTime()
    {
        if (playerNameBGUI.GetComponent<UI_Timer>().timeStop == true)
        {
            gameObject.GetComponent<PlayerController>().gameOver = true;
            gameObject.GetComponent<PlayerWeaponChange>().isDead = true;
            gameObject.GetComponent<PlayerMovement>().isDead = true;
            gameObject.GetComponent<PlayerFire>().isDead = true;
            gameObject.GetComponentInChildren<PlayerLookAimRef>().isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
}