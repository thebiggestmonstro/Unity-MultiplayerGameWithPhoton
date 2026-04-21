using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UI_KillScore : IComparable<UI_KillScore>
{
    public string playerName;
    public int playerKills;

    public UI_KillScore(string newPlayerName, int newPlayerScore)
    {
        playerName = newPlayerName;
        playerKills = newPlayerScore;
    }

    public int CompareTo(UI_KillScore otherKillScore)
    {
        return otherKillScore.playerKills - playerKills;
    }
}
