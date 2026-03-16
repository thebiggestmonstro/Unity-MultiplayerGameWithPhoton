using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputReader _inputReader; 
    private PlayerMovement _movement;
    private PlayerWeaponChange _weaponChange;

    private PhotonView _photonView;
    private CinemachineCamera _cam;
    private GameObject _camObject;

    private RigBuilder _rigBuilder;

    void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _playerInput = GetComponent<PlayerInput>();
        _inputReader = gameObject.AddComponent<InputReader>();
        _movement = GetComponent<PlayerMovement>();
        _weaponChange = GetComponent<PlayerWeaponChange>();
        _rigBuilder = GetComponent<RigBuilder>();
    }

    private void Start()
    {
        if (!_photonView.IsMine)
        {
            if (_playerInput != null)
            {
                _playerInput.enabled = false;
                _inputReader.enabled = false;
                this.enabled = false;

                return;
            }
        }

        _camObject = GameObject.Find("PlayerCam");
        _cam = _camObject.GetComponent<CinemachineCamera>();
        _cam.Follow = gameObject.transform;
        _cam.LookAt = gameObject.transform;
    }

    private void OnEnable()
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            _inputReader.OnJumpPerformed -= _movement.HandleJump;
            _inputReader.OnJumpPerformed += _movement.HandleJump;
            _inputReader.OnSwapPerformed -= _weaponChange.HandleSwap;
            _inputReader.OnSwapPerformed += _weaponChange.HandleSwap;
        }
    }

    private void OnDisable()
    {
        if (this.gameObject.GetComponent<PhotonView>().IsMine)
        {
            _inputReader.OnJumpPerformed -= _movement.HandleJump;
            _inputReader.OnSwapPerformed -= _weaponChange.HandleSwap;
        }
    }

    private void Update()
    {
        if (!gameObject.GetComponent<PhotonView>().IsMine)
        {
            return;
        }

        Vector2 moveDir = _inputReader.MoveInput;
        _movement.UpdateAnimation(moveDir);
    }

    private void FixedUpdate()
    {
        if (!gameObject.GetComponent<PhotonView>().IsMine)
        {
            return;
        }

        Vector2 moveDir = _inputReader.MoveInput;
        Vector2 lookDir = _inputReader.LookInput;

        _movement.CheckGround();
        _movement.ApplyRotation(lookDir);
        _movement.ApplyMovement(moveDir);
    }
}
