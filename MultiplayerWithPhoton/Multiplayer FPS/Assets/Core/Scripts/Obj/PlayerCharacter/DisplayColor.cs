using Photon.Pun;
using UnityEngine;

public class DisplayColor : MonoBehaviour
{
    [SerializeField]
    private Color32[] colors;
    private PhotonView cachedPhotonView;
    private Renderer playerRenderer;

    private void Awake()
    {
        cachedPhotonView = GetComponent<PhotonView>();
        playerRenderer = transform.GetChild(1).GetComponent<Renderer>();
    }

    public void ApplyColor(int colorIndex, int ownerViewID)
    {
        if (cachedPhotonView.ViewID != ownerViewID)
        {
            return;
        }

        playerRenderer.material.color = colors[colorIndex];
    }
}
