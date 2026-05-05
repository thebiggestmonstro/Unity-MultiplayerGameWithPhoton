using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_KillCountPanel : MonoBehaviour
{
    [SerializeField]
    List<UI_KillScore> highestKills = new List<UI_KillScore>();
    [SerializeField]
    TextMeshProUGUI[] playerNames;
    [SerializeField]
    TextMeshProUGUI[] killAmounts;
    [SerializeField]
    GameObject killCountPanel;
    private GameObject namesObject;

    private void Awake()
    {
        UIManager.ResgisterKillCountPanelUI(gameObject.name, this);
    }

    void Start()
    {
        namesObject = UIManager.GetNameBGUI("UI_ImgPlayerNameBG").gameObject;
        killCountPanel.SetActive(false);
    }

    public void UpdateScoreBoard()
    {
        highestKills.Clear();

        for (int i = 0; i < playerNames.Length; i++)
        {
            string playerName = namesObject.GetComponent<UI_NickName>().names[i].text;
            if (playerName != "name" && !string.IsNullOrEmpty(playerName))
            {
                highestKills.Add(new UI_KillScore(playerName, namesObject.GetComponent<UI_NickName>().killScore[i]));
            }
        }

        var sortedPlayerList = highestKills.OrderByDescending(x => x.playerKills).ToList();

        for (int i = 0; i < playerNames.Length; i++)
        {
            if (i < sortedPlayerList.Count)
            {
                playerNames[i].text = sortedPlayerList[i].playerName;
                killAmounts[i].text = sortedPlayerList[i].playerKills.ToString();
            }
            else
            {
                playerNames[i].text = "";
                killAmounts[i].text = "";
            }
        }
    }
}
