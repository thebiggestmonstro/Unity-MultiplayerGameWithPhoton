using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public System.Action OnJumpPerformed;
    public System.Action<int> OnSwapPerformed;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _swapAction;

    void Awake()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["Look"];
        _jumpAction = _playerInput.actions["Jump"];
        _swapAction = _playerInput.actions["Swap"];
    }

    private void OnEnable()
    {        
        _moveAction.performed += Move;
        _moveAction.canceled += StopMove;
        _lookAction.performed += Look;
        _lookAction.canceled += StopLook;
        _jumpAction.performed += Jump;
        _swapAction.performed += Swap;
    }

    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _moveAction.canceled -= StopMove;
        _lookAction.performed -= Look;
        _lookAction.canceled -= StopLook;
        _jumpAction.performed -= Jump;
        _swapAction.performed -= Swap;
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
}
