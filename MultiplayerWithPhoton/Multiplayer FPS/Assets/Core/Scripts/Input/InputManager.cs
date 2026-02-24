using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private InputActionMap playerActionMap;
    private PlayerInput playerInput;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public System.Action OnJumpPerformed;
    public System.Action<int> OnSwapPerformed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        LookInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            OnJumpPerformed?.Invoke();
        }
    }

    public void OnSwap(InputValue value)
    {
        int weaponNumber = (int)value.Get<float>();
        OnSwapPerformed?.Invoke(weaponNumber);
    }
}
