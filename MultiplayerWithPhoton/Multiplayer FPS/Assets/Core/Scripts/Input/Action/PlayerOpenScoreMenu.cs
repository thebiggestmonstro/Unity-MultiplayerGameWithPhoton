using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerOpenScoreMenu : MonoBehaviour
{
    private bool killCountOn = false;
    UI_KillCountPanel killCountPanel;

    private bool teamKillCountOn = false;
    UI_TeamKillCountPanel teamKillCountPanel;

    private void Start()
    {
        killCountPanel = UIManager.GetKillCountPanelUI("UI_KillCountPanelParent");
        teamKillCountPanel = UIManager.GetTeamKillCountPanelUI("UI_TeamKillCountPanelParent");
    }

    public void HandleOpenScoreMenu()
    {
        if (gameObject.GetOrAddComponent<DisplayColor>().teamMode == false)
        {
            killCountOn = !killCountOn;
            killCountPanel.transform.GetChild(0).gameObject.SetActive(killCountOn);

            if (killCountOn)
            {
                killCountPanel.UpdateScoreBoard();
            }
        }
        else if (gameObject.GetOrAddComponent<DisplayColor>().teamMode)
        {
            teamKillCountOn = !teamKillCountOn;
            teamKillCountPanel.transform.GetChild(0).gameObject.SetActive(teamKillCountOn);

            if (teamKillCountOn)
            {
                teamKillCountPanel.UpdateTeamScoreBoard();
            }
        }
    }
}