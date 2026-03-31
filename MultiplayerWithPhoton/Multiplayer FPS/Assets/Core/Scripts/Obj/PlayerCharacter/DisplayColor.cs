using Photon.Pun;
using UnityEngine;

public class DisplayColor : MonoBehaviour
{
    [SerializeField]
    private Color32[] colors;
    private GameObject nickNameUI;
    private PhotonView cachedPhotonView;
    private Renderer playerRenderer;

    private void Awake()
    {
        cachedPhotonView = GetComponent<PhotonView>();
        playerRenderer = transform.GetChild(1).GetComponent<Renderer>();
    }

    private void Start()
    {
        nickNameUI = GameObject.FindWithTag("PlayerNameBG");
    }

    public void ApplyColor(int colorIndex, int ownerViewID)
    {
        if (cachedPhotonView.ViewID != ownerViewID)
        {
            return;
        }

        if (playerRenderer == null)
        {
            return;
        }

        if (nickNameUI == null)
        {
            nickNameUI = GameObject.FindWithTag("PlayerNameBG");
        }

        var uiNickName = nickNameUI.GetComponent<UI_NickName>();

        playerRenderer.material.color = colors[colorIndex];
        uiNickName.names[colorIndex].gameObject.SetActive(true);
        uiNickName.healthbars[colorIndex].gameObject.SetActive(true);
        uiNickName.names[colorIndex].text = cachedPhotonView.Owner.NickName;
    }
}