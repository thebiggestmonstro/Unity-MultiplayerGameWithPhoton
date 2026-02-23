using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerWeaponChange : MonoBehaviour
{
    private InputActionMap playerActionMap;
    private PlayerInput playerInput;

    [SerializeField]
    private TwoBoneIKConstraint leftHand;
    [SerializeField]
    private TwoBoneIKConstraint rightHand;
    [SerializeField]
    private RigBuilder rig;
    [SerializeField]
    private Transform[] leftTargets;
    [SerializeField]
    private Transform[] rightTargets;
    [SerializeField]
    private GameObject[] weapons;

    private int weaponNumber = 0;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerActionMap = playerInput.actions.FindActionMap("Player");
    }

    private void OnDisable()
    {
        playerActionMap.Disable();
    }

    public void OnSwap(InputValue value)
    {
        weaponNumber = (int)value.Get<float>();
        int next1 = (weaponNumber + 1) % 3;
        int next2 = (next1 + 1) % 3;

        weapons[weaponNumber].SetActive(true);
        weapons[next1].SetActive(false);
        weapons[next2].SetActive(false);

        leftHand.data.target = leftTargets[weaponNumber];
        rightHand.data.target = rightTargets[weaponNumber];
        rig.Build();
    }
}
