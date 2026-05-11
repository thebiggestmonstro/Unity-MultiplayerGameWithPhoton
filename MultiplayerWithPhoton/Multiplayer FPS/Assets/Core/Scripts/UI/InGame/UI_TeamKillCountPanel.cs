using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_TeamKillCountPanel : MonoBehaviour
{
    [SerializeField]
    List<UI_KillScore> highestKills = new List<UI_KillScore>();
    [SerializeField]
    TextMeshProUGUI[] killAmounts;
    [SerializeField]
    GameObject killCountPanel;
    private GameObject namesObject;
    [SerializeField]
    private GameObject winnerPanel;
    [SerializeField]
    TextMeshProUGUI winnerText;

    private int RedTeamKills;
    private int BlueTeamKills;

    public bool countDown = true;

    private void Awake()
    {
        UIManager.ResgisterTeamKillCountPanelUI(gameObject.name, this);
    }

    void Start()
    {
        namesObject = UIManager.GetNameBGUI("UI_ImgPlayerNameBG").gameObject;
        killCountPanel.SetActive(false);
        winnerPanel.SetActive(false);
    }

    public void UpdateTeamScoreBoard()
    {
        highestKills.Clear();

        var nickNameComp = namesObject.GetComponent<UI_NickName>();
        int totalPlayers = nickNameComp.names.Length;

        for (int i = 0; i < totalPlayers; i++)
        {
            string playerName = nickNameComp.names[i].text;
            if (playerName != "name" && !string.IsNullOrEmpty(playerName))
            {
                highestKills.Add(new UI_KillScore(playerName, nickNameComp.killScore[i]));
            }
        }

        int half = highestKills.Count / 2;

        RedTeamKills = 0;
        BlueTeamKills = 0;

        for (int i = 0; i < half; i++)
        {
            RedTeamKills += highestKills[i].playerKills;
        }

        for (int i = half; i < highestKills.Count; i++)
        {
            BlueTeamKills += highestKills[i].playerKills;
        }

        killAmounts[0].text = RedTeamKills.ToString();
        killAmounts[1].text = BlueTeamKills.ToString();
    }

    public void TimeOver()
    {
        killCountPanel.SetActive(true);
        winnerPanel.SetActive(true);
        highestKills.Clear();

        var nickNameComp = namesObject.GetComponent<UI_NickName>();
        int totalPlayers = nickNameComp.names.Length;

        for (int i = 0; i < totalPlayers; i++)
        {
            highestKills.Add(new UI_KillScore(nickNameComp.names[i].text, nickNameComp.killScore[i]));
        }

        int half = highestKills.Count / 2;

        RedTeamKills = 0;
        BlueTeamKills = 0;

        for (int i = 0; i < half; i++)
        {
            RedTeamKills += highestKills[i].playerKills;
        }
        for (int i = half; i < highestKills.Count; i++)
        {
            BlueTeamKills += highestKills[i].playerKills;
        }

        killAmounts[0].text = RedTeamKills.ToString();
        killAmounts[1].text = BlueTeamKills.ToString();

        if (RedTeamKills > BlueTeamKills)
        {
            winnerText.text = "RED TEAM WINS";
            winnerText.alignment = TextAlignmentOptions.Center;
        }
        else if (RedTeamKills < BlueTeamKills)
        {
            winnerText.text = "BLUE TEAM WINS";
            winnerText.alignment = TextAlignmentOptions.Center;
        }
        else
        {
            winnerText.text = "DRAW";
            winnerText.alignment = TextAlignmentOptions.Center;
        }
    }
}
