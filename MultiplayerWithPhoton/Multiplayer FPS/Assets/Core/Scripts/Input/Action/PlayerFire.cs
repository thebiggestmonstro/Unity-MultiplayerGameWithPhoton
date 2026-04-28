using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    GameObject[] muzzleFlash;

    private PhotonView _photonView;
    private DisplayColor _displayColor;
    private int _currentWeaponNumber = 0;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _displayColor = GetComponent<DisplayColor>();
    }

    public void HandleFire(int weaponNumber)
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        _photonView.RPC(nameof(FireRPC), RpcTarget.All, weaponNumber);
        _displayColor.PlayGunShot(_photonView.Owner.NickName, weaponNumber);
    } 

    [PunRPC]
    private void FireRPC(int weaponNumber)
    {
        _currentWeaponNumber = weaponNumber;
        StartCoroutine(CoMuzzleFlash(_currentWeaponNumber));
    }

    private IEnumerator CoMuzzleFlash(int weaponNumber)
    {
        muzzleFlash[weaponNumber].SetActive(true);
        yield return new WaitForSeconds(0.03f);
        muzzleFlash[weaponNumber].SetActive(false);
    }
}
