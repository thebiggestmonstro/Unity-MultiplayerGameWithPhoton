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

    private void Start()
    {
        killCountPanel = UIManager.GetKillCountPanelUI("UI_KillCountPanelParent");
    }

    public void HandleOpenScoreMenu()
    {
        killCountOn = !killCountOn;
        killCountPanel.transform.GetChild(0).gameObject.SetActive(killCountOn);

        if (killCountOn)
        {
            killCountPanel.UpdateScoreBoard();
        }
    }
}