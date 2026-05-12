using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using Photon.Pun;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputReader _inputReader; 
    private PlayerMovement _movement;
    private PlayerWeaponChange _weaponChange;
    private PlayerOpenScoreMenu _openScoreMenu;
    private PlayerExitGame _exitGame;
    private PlayerFire _fire;

    private PhotonView _photonView;
    private CinemachineCamera _cam;
    private GameObject _camObject;

    private RigBuilder _rigBuilder;
    private Vector3 startPos;
    private bool respawned = false;
    private GameObject respawnPanel;
    private GameObject killCountPanel;
    private bool startChecking = false;

    public bool gameOver = false;
    public bool noRespawn;

    void Awake()
    {
        _photonView = gameObject.GetOrAddComponent<PhotonView>();
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        _inputReader = gameObject.GetOrAddComponent<InputReader>();
        _movement = gameObject.GetOrAddComponent<PlayerMovement>();
        _weaponChange = gameObject.GetOrAddComponent<PlayerWeaponChange>();
        _openScoreMenu = gameObject.GetOrAddComponent<PlayerOpenScoreMenu>();
        _exitGame = gameObject.GetOrAddComponent<PlayerExitGame>();
        _fire = gameObject.GetOrAddComponent<PlayerFire>();
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

        respawnPanel = UIManager.GetRespawnPanelUI("UI_RespawnPanel").gameObject;
        killCountPanel = UIManager.GetKillCountPanelUI("UI_KillCountPanelParent").gameObject;
        startPos = gameObject.transform.position;
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
            _inputReader.OnExitGamePerformed -= _exitGame.HandleExitGame;
            _inputReader.OnExitGamePerformed += _exitGame.HandleExitGame;
            _inputReader.OnFirePerformed -= _fire.HandleFire;
            _inputReader.OnFirePerformed += _fire.HandleFire;
        }
    }

    private void OnDisable()
    {
        if (_photonView.IsMine)
        {
            _inputReader.OnJumpPerformed -= _movement.HandleJump;
            _inputReader.OnSwapPerformed -= _weaponChange.HandleSwap;
            _inputReader.OnOpenScoreMenuPerforemd -= _openScoreMenu.HandleOpenScoreMenu;
            _inputReader.OnExitGamePerformed -= _exitGame.HandleExitGame;
            _inputReader.OnFirePerformed -= _fire.HandleFire;
        }
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            return;
        }

        if (_movement.isDead == false)
        {
            respawnPanel.SetActive(false);
            Vector2 moveDir = _inputReader.MoveInput;
            _movement.UpdateAnimation(moveDir);
        }

        if (_movement.isDead && respawned == false && gameOver == false && noRespawn == false)
        {
            respawned = !respawned;
            respawnPanel.SetActive(true);
            respawnPanel.GetComponent<UI_RespawnTimer>().enabled = true;
            StartCoroutine(RespawnWait());
        }

        if (_movement.isDead && respawned == false && gameOver == false && noRespawn == true)
        {
            respawned = true;
            GetComponent<DisplayColor>().NoRespawnExit();
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && startChecking == false)
        {
            startChecking = true;
            InvokeRepeating("CheckForWinner", 3, 3);
        }
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

    IEnumerator RespawnWait()
    {
        yield return new WaitForSeconds(3);
        _movement.isDead = false;
        _weaponChange.isDead = false;
        _fire.isDead = false;
        respawned = !respawned;
        gameObject.transform.position = startPos;
        GetComponent<DisplayColor>().Respawn(_photonView.Owner.NickName);
    }

    void CheckForWinner()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && noRespawn)
        {
            killCountPanel.GetComponent<UI_KillCountPanel>().NoRespawnWinner(GetComponent<PhotonView>().Owner.NickName);
        }
    }
}
