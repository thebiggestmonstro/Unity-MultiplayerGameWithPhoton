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
    int totalSeconds = 299; 
    [SerializeField]
    GameObject killCountPanel;
    [HideInInspector]
    public bool timeStop = false;

    private double startTime;
    private bool isTimerRunning = false;

    public void BeginTimer()
    {
        gameObject.GetOrAddComponent<PhotonView>().RPC("RPC_StartTimer", RpcTarget.AllBuffered, PhotonNetwork.Time);
    }

    [PunRPC]
    void RPC_StartTimer(double serverStartTime)
    {
        startTime = serverStartTime;
        isTimerRunning = true;
    }

    void Update()
    {
        if (!isTimerRunning)
        {
            return;
        }

        double elapsedTime = PhotonNetwork.Time - startTime;
        int remainingTime = Mathf.Max(0, totalSeconds - (int)elapsedTime);

        UpdateUI(remainingTime);

        if (remainingTime <= 0)
        {
            OnTimerEnd();
        }
    }

    void UpdateUI(int timeInSeconds)
    {
        int min = timeInSeconds / 60;
        int sec = timeInSeconds % 60;

        minutesText.text = min.ToString();
        secondsText.text = sec.ToString("D2");
    }

    void OnTimerEnd()
    {
        if (!isTimerRunning)
        {
            return;
        }

        timeStop = true;
        isTimerRunning = false;
        var killPanelScript = killCountPanel.GetComponent<UI_KillCountPanel>();
        killPanelScript.countDown = false;
        killPanelScript.TimeOver();
    }
}
