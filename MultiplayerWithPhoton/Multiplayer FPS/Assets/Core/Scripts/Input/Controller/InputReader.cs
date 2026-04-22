using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public System.Action OnJumpPerformed;
    public System.Action<int> OnSwapPerformed;
    public System.Action OnOpenScoreMenuPerforemd;
    public System.Action OnExitGamePerformed;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _swapAction;
    private InputAction _openScoreMenuAction;
    private InputAction _exitGameAction;

    void Awake()
    {
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["Look"];
        _jumpAction = _playerInput.actions["Jump"];
        _swapAction = _playerInput.actions["Swap"];
        _openScoreMenuAction = _playerInput.actions["OpenScoreMenu"];
        _exitGameAction = _playerInput.actions["Exit"];
    }

    private void OnEnable()
    {        
        _moveAction.performed += Move;
        _moveAction.canceled += StopMove;
        _lookAction.performed += Look;
        _lookAction.canceled += StopLook;
        _jumpAction.performed += Jump;
        _swapAction.performed += Swap;
        _openScoreMenuAction.performed += OpenScoreMenu;
        _exitGameAction.performed += ExitGame;
    }

    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _moveAction.canceled -= StopMove;
        _lookAction.performed -= Look;
        _lookAction.canceled -= StopLook;
        _jumpAction.performed -= Jump;
        _swapAction.performed -= Swap;
        _openScoreMenuAction.performed -= OpenScoreMenu;
        _exitGameAction.performed -= ExitGame;
    }

    private void Move(InputAction.CallbackContext context)
    { 
        MoveInput = context.ReadValue<Vector2>();
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    private void Look(InputAction.CallbackContext context)
    { 
        LookInput = context.ReadValue<Vector2>();
    }

    private void StopLook(InputAction.CallbackContext context)
    { 
        LookInput = Vector2.zero;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJumpPerformed?.Invoke();
    }

    private void Swap(InputAction.CallbackContext context)
    {
        int weaponNumber = (int)context.ReadValue<float>();
        OnSwapPerformed?.Invoke(weaponNumber);
    }

    private void OpenScoreMenu(InputAction.CallbackContext context)
    {
        OnOpenScoreMenuPerforemd?.Invoke();
    }

    private void ExitGame(InputAction.CallbackContext context)
    { 
        OnExitGamePerformed?.Invoke();
    }
}
