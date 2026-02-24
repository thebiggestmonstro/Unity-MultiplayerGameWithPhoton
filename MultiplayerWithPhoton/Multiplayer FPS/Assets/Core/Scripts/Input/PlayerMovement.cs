using UnityEngine;

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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        InputManager.Instance.OnJumpPerformed -= HandleJump;
        InputManager.Instance.OnJumpPerformed += HandleJump;
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPerformed += HandleJump;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPerformed -= HandleJump;
        }
    }

    private void HandleJump()
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer);

        Vector2 dir = InputManager.Instance.MoveInput;
        anim.SetFloat("BlendVertical", dir.y, 0.1f, Time.deltaTime);
        anim.SetFloat("BlendHorizontal", dir.x, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ApplyRotation(InputManager.Instance.LookInput);
        ApplyMovement(InputManager.Instance.MoveInput);
    }

    private void ApplyMovement(Vector2 dir)
    {
        Vector3 moveDir = (transform.forward * dir.y) + (transform.right * dir.x);
        Vector3 destination = moveDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + destination);
    }

    private void ApplyRotation(Vector2 lookDir)
    {
        float rotateY = lookDir.x * rotateSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotateY, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
