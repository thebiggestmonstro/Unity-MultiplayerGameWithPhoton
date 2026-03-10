using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement movement;
    private PlayerWeaponChange weaponChange;

    private CinemachineCamera cam;
    private GameObject camObject;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        weaponChange = GetComponent<PlayerWeaponChange>();
    }

    private void Start()
    {
        InputManager.Instance.BindPlayerInput(playerInput);

        camObject = GameObject.Find("PlayerCam");

        if (this.gameObject.GetComponent<PhotonView>().IsMine == true)
        {
            cam = camObject.GetComponent<CinemachineCamera>();
            cam.Follow = gameObject.transform;
            cam.LookAt = gameObject.transform;
        }
        else
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            playerInput.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPerformed -= movement.HandleJump;
            InputManager.Instance.OnJumpPerformed += movement.HandleJump;
            InputManager.Instance.OnSwapPerformed -= weaponChange.HandleSwap;
            InputManager.Instance.OnSwapPerformed += weaponChange.HandleSwap;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPerformed -= movement.HandleJump;
            InputManager.Instance.OnSwapPerformed -= weaponChange.HandleSwap;
        }
    }

    private void Update()
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine == true)
        {
            Vector2 moveDir = InputManager.Instance.MoveInput;
            movement.UpdateAnimation(moveDir);
        }
    }

    private void FixedUpdate()
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine == true)
        {
            Vector2 moveDir = InputManager.Instance.MoveInput;
            Vector2 lookDir = InputManager.Instance.LookInput;

            movement.CheckGround();
            movement.ApplyRotation(lookDir);
            movement.ApplyMovement(moveDir);
        }
    }
}
