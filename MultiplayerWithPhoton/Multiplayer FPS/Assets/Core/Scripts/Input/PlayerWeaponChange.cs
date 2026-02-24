using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerWeaponChange : MonoBehaviour
{
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

    private void Start()
    {
        InputManager.Instance.OnSwapPerformed -= HandleSwap;
        InputManager.Instance.OnSwapPerformed += HandleSwap;
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSwapPerformed += HandleSwap;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnSwapPerformed -= HandleSwap;
        }
    }

    private void HandleSwap(int weaponNumber)
    {
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
