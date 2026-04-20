using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class ObjectManager
{
    private static Dictionary<string, WeaponPickup> weaponDict = new Dictionary<string, WeaponPickup>();
    private static Dictionary<string, SpawnCharacter> characterSpawnerDict = new Dictionary<string, SpawnCharacter>();
    private static Dictionary<string, SpawnWeapon> weaponSpawnerDict = new Dictionary<string, SpawnWeapon>();

    public static void RegisterWeapon(string weaponName, WeaponPickup weapon)
    {
        if (!weaponDict.ContainsKey(weaponName))
        {
            weaponDict.Add(weaponName, weapon);
        }
    }

    public static void RegisterCharacterSpawner(string spawnerName, SpawnCharacter spawner)
    {
        if (!characterSpawnerDict.ContainsKey(spawnerName))
        {
            characterSpawnerDict.Add(spawnerName, spawner);
        }
    }

    public static void RegisterWeaponSpawner(string spawnerName, SpawnWeapon spawner)
    {
        if (!weaponSpawnerDict.ContainsKey(spawnerName))
        {
            weaponSpawnerDict.Add(spawnerName, spawner);
        }
    }

    public static void UnregisterWeapon(string weaponName)
    {
        weaponDict.Remove(weaponName);
    }

    public static void UnregisterCharacterSpawner(string spawnerName)
    {
        characterSpawnerDict.Remove(spawnerName);
    }

    public static void UnRegisterWeaponSpawner(string spawnerName)
    {
        weaponSpawnerDict.Remove(spawnerName);
    }

    public static WeaponPickup GetWeapon(string weaponName)
    {
        WeaponPickup weapon = null;
        weaponDict.TryGetValue(weaponName, out weapon);
        return weapon;
    }

    public static SpawnCharacter GetCharacterSpawner(string spawnerName)
    {
        SpawnCharacter spawner = null;
        characterSpawnerDict.TryGetValue(spawnerName, out spawner);
        return spawner;
    }

    public static SpawnWeapon GetWeaponSpawner(string spawnerName)
    {
        SpawnWeapon spawner = null;
        weaponSpawnerDict.TryGetValue(spawnerName, out spawner);
        return spawner;
    }
}
