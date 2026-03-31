using Photon.Pun;
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI minutesText;
    [SerializeField]
    TextMeshProUGUI secondsText;
    [SerializeField]
    int minutes = 4;
    [SerializeField]
    int seconds = 59;

    public void BeginTimer()
    {
        gameObject.GetOrAddComponent<PhotonView>().RPC("Count", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void Count()
    {
        BeginCounting();
    }

    void BeginCounting()
    {
        CancelInvoke();
        InvokeRepeating("TimeCountDown", 1, 1);
    }

    void TimeCountDown()
    {
        if (seconds > 0)
        {
            seconds -= 1;
        }
        else if (minutes > 0)
        {
            minutes -= 1;
            seconds = 59;
        }

        secondsText.text = seconds.ToString("D2");
        minutesText.text = minutes.ToString(); 
    }
}
