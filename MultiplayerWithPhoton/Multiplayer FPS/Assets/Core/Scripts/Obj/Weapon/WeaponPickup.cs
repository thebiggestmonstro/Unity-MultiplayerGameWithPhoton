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
    [SerializeField]
    int ammoRefillAmt = 10;

    private void Awake()
    {
        ObjectManager.RegisterWeapon(gameObject.name, this);
    }

    void Start()
    {
        _audioPlayer = gameObject.GetOrAddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetOrAddComponent<PhotonView>().RPC("PlayPickupAudio",RpcTarget.All);
            gameObject.GetOrAddComponent<PhotonView>().RPC("DisableWeapon", RpcTarget.All);
            other.GetComponent<PlayerWeaponChange>().ammoAmounts[weaponType - 1] += ammoRefillAmt;
            other.GetComponent<PlayerWeaponChange>().UpdatePickup();
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
            gameObject.GetOrAddComponent<Renderer>().enabled = false;
            gameObject.GetOrAddComponent<Collider>().enabled = false;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetOrAddComponent<Collider>().enabled = false;
        }
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        gameObject.GetOrAddComponent<PhotonView>().RPC("EnableWeapon", RpcTarget.All);
    }

    [PunRPC]
    void EnableWeapon()
    {
        if (weaponType == 1)
        {
            gameObject.GetOrAddComponent<Renderer>().enabled = true;
            gameObject.GetOrAddComponent<Collider>().enabled = true;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetOrAddComponent<Collider>().enabled = true;
        }
    }
}
