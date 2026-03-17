using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponChange : MonoBehaviour
{
    [SerializeField]
    private TwoBoneIKConstraint leftHand;
    [SerializeField]
    private TwoBoneIKConstraint rightHand;
    [SerializeField]
    private TwoBoneIKConstraint leftThumb;
    [SerializeField]
    private RigBuilder rig;
    [SerializeField]
    private Transform[] leftTargets;
    [SerializeField]
    private Transform[] rightTargets;
    [SerializeField]
    private Transform[] thumbTargets;
    [SerializeField]
    private GameObject[] weapons;
    private GameObject _testForWeapons;

    private void Start()
    {
        _testForWeapons = GameObject.Find("Weapon1Pickup(Clone)");
        if (_testForWeapons == null)
        {
            var spawner = GameObject.Find("WeaponSpawner1");
            spawner.GetComponent<SpawnWeapon>().SpawnWeaponsStart();
        }

    }

    public void HandleSwap(int weaponNumber)
    {
        GetComponent<PhotonView>().RPC("SwapWeapon", RpcTarget.AllBuffered, weaponNumber);
    }

    [PunRPC]
    public void SwapWeapon(int weaponNumber)
    {
        int next1 = (weaponNumber + 1) % 3;
        int next2 = (next1 + 1) % 3;

        weapons[weaponNumber].SetActive(true);
        weapons[next1].SetActive(false);
        weapons[next2].SetActive(false);

        leftHand.data.target = leftTargets[weaponNumber];
        rightHand.data.target = rightTargets[weaponNumber];
        leftThumb.data.target = thumbTargets[weaponNumber];
        rig.Build();
    }
}
