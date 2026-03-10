using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public System.Action OnJumpPerformed;
    public System.Action<int> OnSwapPerformed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void BindPlayerInput(PlayerInput playerInput)
    {
        playerInput.actions["Move"].performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Move"].canceled += ctx => MoveInput = Vector2.zero;

        playerInput.actions["Look"].performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        playerInput.actions["Look"].canceled += ctx => LookInput = Vector2.zero;

        playerInput.actions["Jump"].performed += ctx => OnJumpPerformed?.Invoke();

        playerInput.actions["Swap"].performed += ctx => {
            int weaponNumber = (int)ctx.ReadValue<float>();
            OnSwapPerformed?.Invoke(weaponNumber);
        };
    }
}
