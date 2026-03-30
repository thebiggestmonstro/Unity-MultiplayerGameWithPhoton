using UnityEngine;
using Photon.Pun;
using System.Collections;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    [SerializeField]
    Transform[] spawnPoints;

    private void Awake()
    {
        ObjectManager.RegisterCharacterSpawner(gameObject.name, this);
    }

    private void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(1);
        
        if (PhotonNetwork.IsConnected)
        {
            int spawnIndex = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length;
            PhotonNetwork.Instantiate(character.name, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        }
    }
}
