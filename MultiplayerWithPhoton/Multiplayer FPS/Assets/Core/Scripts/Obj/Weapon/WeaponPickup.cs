using Photon.Pun;
using System.Collections;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private AudioSource _audioPlayer;
    [SerializeField]
    float _respawnTime = 5;
    [SerializeField]
    int weaponType = 1;

    void Start()
    {
        _audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<PhotonView>().RPC("PlayPickupAudio",RpcTarget.All);
            gameObject.GetComponent<PhotonView>().RPC("DisableWeapon", RpcTarget.All);
        }
    }

    [PunRPC]
    void PlayPickupAudio()
    {
        _audioPlayer.Play();
    }

    [PunRPC]
    void DisableWeapon()
    {
        if (weaponType == 1)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
        }
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        gameObject.GetComponent<PhotonView>().RPC("EnableWeapon", RpcTarget.All);
    }

    [PunRPC]
    void EnableWeapon()
    {
        if (weaponType == 1)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
