using Photon.Pun;
using UnityEngine;

public class DisplayColor : MonoBehaviour
{
    [SerializeField]
    private Color32[] colors;
    private UI_NickName nickNameUI;
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
}