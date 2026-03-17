using Photon.Pun;
using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject[] weapons;
    [SerializeField]
    Transform[] weaponSpawnPoints;
    [SerializeField]
    float weaponRespawnTime = 10;

    public void SpawnWeaponsStart()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            PhotonNetwork.Instantiate(weapons[i].name, weaponSpawnPoints[i].position, weaponSpawnPoints[i].rotation);
        }
    }
}
