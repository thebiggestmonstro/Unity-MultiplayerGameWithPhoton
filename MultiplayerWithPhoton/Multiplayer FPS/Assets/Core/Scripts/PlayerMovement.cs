using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;
    [SerializeField]
    private float rotateSpeed = 100.0f;
    [SerializeField] 
    private float jumpForce = 5.0f; 
    [SerializeField] 
    private LayerMask groundLayer;
    [SerializeField] 
    private float groundCheckRadius = 0.2f;
    [SerializeField] 
    private Vector3 groundCheckOffset = new Vector3(0, 0.1f, 0);

    private bool isGrounded;

    private Rigidbody rb;
    private Animator anim;
    private Vector2 direction;
    private Vector2 lookDirection;

    private InputActionMap playerActionMap;
    private PlayerInput playerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
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
        direction = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookDirection = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer);

        anim.SetFloat("BlendVertical", direction.y, 0.1f, Time.deltaTime);
        anim.SetFloat("BlendHorizontal", direction.x, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ApplyRotation();
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Vector3 moveDir = (transform.forward * direction.y) + (transform.right * direction.x);
        Vector3 destination = moveDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + destination);
    }

    private void ApplyRotation()
    {
        float rotateY = lookDirection.x * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotateY, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
