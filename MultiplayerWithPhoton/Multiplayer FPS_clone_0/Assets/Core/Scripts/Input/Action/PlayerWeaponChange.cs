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

    [SerializeField]
    MultiAimConstraint[] aimObjects;
    private Transform aimTarget;

    private void Start()
    {
        aimTarget = GameObject.Find("PlayerAimRef").transform;
        Invoke("SetLookAt", 0.1f);
    }

    void SetLookAt()
    {
        if (aimTarget != null)
        {
            for (int i = 0; i < aimObjects.Length; i++)
            {
                var target = aimObjects[i].data.sourceObjects;
                target.SetTransform(0, aimTarget.transform);
                aimObjects[i].data.sourceObjects = target;
            }
        }

        rig.Build();
    }

    public void HandleSwap(int weaponNumber)
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
