using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static Dictionary<string, UI_NickName> nickNameUIDict = new Dictionary<string, UI_NickName>();
    private static Dictionary<string, UI_PlayerAim> aimUIDict = new Dictionary<string, UI_PlayerAim>();
    private static Dictionary<string, UI_WeaponAmmo> ammoUIDict =  new Dictionary<string, UI_WeaponAmmo>();
    private static Dictionary<string, UI_WeaponSlot> slotUIDict =  new Dictionary<string, UI_WeaponSlot>();
    private static Dictionary<string, UI_PlayerNameBG> nameBGUIDict =  new Dictionary<string, UI_PlayerNameBG>();
    private static Dictionary<string, UI_KillCountPanel> killCountPanelDict = new Dictionary<string, UI_KillCountPanel>();
    private static Dictionary<string, UI_PlayerCheck> playerCheckDict = new Dictionary<string, UI_PlayerCheck>();

    public static void RegisterNickNameUI(string nickNameUIName, UI_NickName nickNameUI)
    {
        if (!nickNameUIDict.ContainsKey(nickNameUIName))
        {
            nickNameUIDict.Add(nickNameUIName, nickNameUI);
        }
    }

    public static void UnregisterNickNameUI(string nickNameUIName)
    {
        nickNameUIDict.Remove(nickNameUIName);
    }

    public static UI_NickName GetNickNameUI(string nickNameUIName)
    {
        UI_NickName nickNameUI = null;
        nickNameUIDict.TryGetValue(nickNameUIName, out nickNameUI);
        return nickNameUI;
    }

    public static void RegisterAimUI(string aimUIName, UI_PlayerAim aimUI)
    {
        if (!aimUIDict.ContainsKey(aimUIName))
        {
            aimUIDict.Add(aimUIName, aimUI);
        }
    }

    public static void UnregisterAimUI(string aimUIName)
    {
        aimUIDict.Remove(aimUIName);
    }

    public static UI_PlayerAim GetAimUI(string aimUIName)
    {
        UI_PlayerAim aimUI = null;
        aimUIDict.TryGetValue(aimUIName, out aimUI);
        return aimUI;
    }

    public static void RegisterAmmoUI(string ammoUIName, UI_WeaponAmmo ammoUI)
    {
        if (!ammoUIDict.ContainsKey(ammoUIName))
        {
            ammoUIDict.Add(ammoUIName, ammoUI);
        }
    }

    public static void UnregisterAmmoUI(string ammoUIName)
    {
        ammoUIDict.Remove(ammoUIName);
    }

    public static UI_WeaponAmmo GetAmmoUI(string ammoUIName)
    {
        UI_WeaponAmmo ammoUI = null;
        ammoUIDict.TryGetValue(ammoUIName, out ammoUI);
        return ammoUI;
    }

    public static void RegisterSlotUI(string slotUIName, UI_WeaponSlot slotUI)
    {
        if (!slotUIDict.ContainsKey(slotUIName))
        {
            slotUIDict.Add(slotUIName, slotUI);
        }
    }

    public static void UnregisterSlotUI(string slotUIName)
    {
        slotUIDict.Remove(slotUIName);
    }

    public static UI_WeaponSlot GetSlotUI(string slotUIName)
    {
        UI_WeaponSlot slotUI = null;
        slotUIDict.TryGetValue(slotUIName, out slotUI);
        return slotUI;
    }

    public static void ResgisterNameBGUI(string BGUIName, UI_PlayerNameBG nameBGUI)
    {
        if (!nameBGUIDict.ContainsKey(BGUIName))
        {
            nameBGUIDict.Add(BGUIName, nameBGUI);
        }
    }

    public static void UnResgisterNameBGUI(string BGUIName)
    {
        nameBGUIDict.Remove(BGUIName);
    }

    public static UI_PlayerNameBG GetNameBGUI(string BGUIName)
    {
        UI_PlayerNameBG BGUI = null;
        nameBGUIDict.TryGetValue(BGUIName, out BGUI);
        return BGUI;
    }

    public static void ResgisterKillCountPanelUI(string killCountPanelName, UI_KillCountPanel killCountPanelUI)
    {
        if (!killCountPanelDict.ContainsKey(killCountPanelName))
        {
            killCountPanelDict.Add(killCountPanelName, killCountPanelUI);
        }
    }

    public static void UnResgisterKillCountPanelUI(string killCountPanelName)
    {
        killCountPanelDict.Remove(killCountPanelName);
    }

    public static UI_KillCountPanel GetKillCountPanelUI(string killCountPanelName)
    {
        UI_KillCountPanel killCountPanelUI = null;
        killCountPanelDict.TryGetValue(killCountPanelName, out killCountPanelUI);
        return killCountPanelUI;
    }

    public static void ResgisterPlayerCheckUI(string playerCheckUIName, UI_PlayerCheck playerCheckUI)
    {
        if (!playerCheckDict.ContainsKey(playerCheckUIName))
        {
            playerCheckDict.Add(playerCheckUIName, playerCheckUI);
        }
    }

    public static void UnResgisterPlayerCheckUI(string playerCheckUIName)
    {
        playerCheckDict.Remove(playerCheckUIName);
    }

    public static UI_PlayerCheck GetPlayerCheckUI(string playerCheckUIName)
    {
        UI_PlayerCheck playerCheckUI = null;
        playerCheckDict.TryGetValue(playerCheckUIName, out playerCheckUI);
        return playerCheckUI;
    }
}
