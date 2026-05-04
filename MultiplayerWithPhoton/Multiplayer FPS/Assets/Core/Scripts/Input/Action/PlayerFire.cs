using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    GameObject[] muzzleFlash;
    [SerializeField] 
    float[] damageAmts;
    private string shooterName;
    private string targetName;

    private PhotonView _photonView;
    private DisplayColor _displayColor;
    private int _currentWeaponNumber = 0;

    public bool isDead = false;

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

        if (isDead)
        {
            return;
        }

        _photonView.RPC(nameof(FireRPC), RpcTarget.All, weaponNumber);
        _displayColor.PlayGunShot(_photonView.Owner.NickName, weaponNumber);

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit, 500))
        {
            if (hit.transform.gameObject.GetComponent<PhotonView>() != null)
            {
                targetName =  hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName;
            }

            if (hit.transform.gameObject.GetComponent<DisplayColor>() != null)
            {
                hit.transform.gameObject.GetComponent<DisplayColor>().DeliverDamage(hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName, damageAmts[weaponNumber]);
            }

            shooterName = GetComponent<PhotonView>().Owner.NickName;
            Debug.Log(targetName + " got hit by " + shooterName);
        }

        gameObject.layer = LayerMask.NameToLayer("Default");
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
