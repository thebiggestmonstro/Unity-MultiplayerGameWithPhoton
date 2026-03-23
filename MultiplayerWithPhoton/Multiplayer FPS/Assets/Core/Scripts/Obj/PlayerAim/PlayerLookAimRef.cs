using Photon.Pun;
using UnityEngine;

public class PlayerLookAimRef : MonoBehaviour
{
    private GameObject _playerAimObject;
    private PhotonView _photonView;

    void Start()
    {
        _playerAimObject = GameObject.FindWithTag("PlayerAim");
        _photonView = Util.FindParent<PhotonView>(this.gameObject, null, false);
    }

    void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            transform.position = _playerAimObject.transform.position;
        }
    }
}
