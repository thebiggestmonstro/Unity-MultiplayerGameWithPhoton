using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float rotateSpeed = 70.0f;
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

    public bool isDead = false;

    void Awake()
    {
        rb = gameObject.GetOrAddComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = gameObject.GetOrAddComponent<Animator>();
    }

    public void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer);
    }

    public void UpdateAnimation(Vector2 dir)
    {
        if (isDead == false)
        {
            anim.SetFloat("BlendVertical", dir.y, 0.1f, Time.deltaTime);
            anim.SetFloat("BlendHorizontal", dir.x, 0.1f, Time.deltaTime);
        }
    }

    public void ApplyMovement(Vector2 dir)
    {
        if (isDead == false)
        {
            Vector3 moveDir = (transform.forward * dir.y) + (transform.right * dir.x);
            Vector3 destination = moveDir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + destination);
        }
    }

    public void ApplyRotation(Vector2 lookDir)
    {
        if (isDead == false)
        {
            float rotateY = lookDir.x * rotateSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotateY, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

    public void HandleJump()
    {
        if (isGrounded && isDead == false)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}
