using UnityEngine;
using Photon.Pun;
using System.Collections;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject character;
    [SerializeField]
    Transform[] spawnPoints;
    
    private void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(1);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CountOfPlayers - 1].position, spawnPoints[PhotonNetwork.CountOfPlayers - 1].rotation);
        }
    }
}
