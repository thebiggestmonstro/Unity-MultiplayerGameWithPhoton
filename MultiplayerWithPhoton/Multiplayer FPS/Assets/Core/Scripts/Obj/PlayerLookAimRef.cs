using Photon.Pun;
using UnityEngine;

public class PlayerLookAimRef : MonoBehaviour
{
    private GameObject _playerAimObject;

    void Start()
    {
        _playerAimObject = GameObject.Find("PlayerAim");
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponentInParent<PhotonView>().IsMine)
        {
            transform.position = _playerAimObject.transform.position;
        }
    }
}
