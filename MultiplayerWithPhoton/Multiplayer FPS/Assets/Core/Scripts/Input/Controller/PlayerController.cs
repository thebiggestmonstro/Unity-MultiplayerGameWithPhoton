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
    private PlayerOpenScoreMenu _openScoreMenu;

    private PhotonView _photonView;
    private CinemachineCamera _cam;
    private GameObject _camObject;

    private RigBuilder _rigBuilder;

    void Awake()
    {
        _photonView = gameObject.GetOrAddComponent<PhotonView>();
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        _inputReader = gameObject.GetOrAddComponent<InputReader>();
        _movement = gameObject.GetOrAddComponent<PlayerMovement>();
        _weaponChange = gameObject.GetOrAddComponent<PlayerWeaponChange>();
        _openScoreMenu = gameObject.GetOrAddComponent<PlayerOpenScoreMenu>();
        _rigBuilder = gameObject.GetOrAddComponent<RigBuilder>();
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

        _camObject = GameObject.FindWithTag("FollowCamera");
        _cam = _camObject.GetOrAddComponent<CinemachineCamera>();
        _cam.Follow = gameObject.transform;
        _cam.LookAt = gameObject.transform;
    }

    private void OnEnable()
    {
        if (_photonView.IsMine)
        {
            _inputReader.OnJumpPerformed -= _movement.HandleJump;
            _inputReader.OnJumpPerformed += _movement.HandleJump;
            _inputReader.OnSwapPerformed -= _weaponChange.HandleSwap;
            _inputReader.OnSwapPerformed += _weaponChange.HandleSwap;
            _inputReader.OnOpenScoreMenuPerforemd -= _openScoreMenu.HandleOpenScoreMenu;
            _inputReader.OnOpenScoreMenuPerforemd += _openScoreMenu.HandleOpenScoreMenu;
        }
    }

    private void OnDisable()
    {
        if (_photonView.IsMine)
        {
            _inputReader.OnJumpPerformed -= _movement.HandleJump;
            _inputReader.OnSwapPerformed -= _weaponChange.HandleSwap;
            _inputReader.OnOpenScoreMenuPerforemd -= _openScoreMenu.HandleOpenScoreMenu;
        }
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        Vector2 moveDir = _inputReader.MoveInput;
        _movement.UpdateAnimation(moveDir);
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine)
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
