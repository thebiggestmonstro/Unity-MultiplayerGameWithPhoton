using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

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
    [SerializeField]
    private Sprite[] weaponIcons;
    [SerializeField]
    private int[] ammoAmounts;

    private WeaponPickup _testForWeapons;
    private Image weaponIcon;
    private TextMeshProUGUI ammoAmount;

    private void Start()
    {
        weaponIcon = GameObject.FindGameObjectWithTag("WeaponSlot").GetOrAddComponent<Image>();
        ammoAmount = GameObject.FindGameObjectWithTag("WeaponAmmo").GetOrAddComponent<TextMeshProUGUI>();

        _testForWeapons = ObjectManager.GetWeapon("Weapon1Pickup(Clone)");
        if (_testForWeapons == null)
        {
            var spawner = ObjectManager.GetWeaponSpawner("WeaponSpawner1");
            spawner.SpawnWeaponsStart();
        }

    }

    public void HandleSwap(int weaponNumber)
    {
        gameObject.GetOrAddComponent<PhotonView>().RPC("SwapWeapon", RpcTarget.AllBuffered, weaponNumber);
    }

    [PunRPC]
    public void SwapWeapon(int weaponNumber)
    {
        int next1 = (weaponNumber + 1) % 3;
        int next2 = (next1 + 1) % 3;

        weapons[weaponNumber].SetActive(true);
        weapons[next1].SetActive(false);
        weapons[next2].SetActive(false);

        weaponIcon.sprite = weaponIcons[weaponNumber];
        ammoAmount.text = ammoAmounts[weaponNumber].ToString();

        leftHand.data.target = leftTargets[weaponNumber];
        rightHand.data.target = rightTargets[weaponNumber];
        leftThumb.data.target = thumbTargets[weaponNumber];
        rig.Build();
    }
}
