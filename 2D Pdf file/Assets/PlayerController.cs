using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the player movement
    public float jumpForce = 10f; // Force of the jump
    public LayerMask groundLayer; // Ground layer to check if the player is on the ground
    public Transform groundCheck; // Position to check if player is grounded
    
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isIdle = true; // Initially set the player as idle
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        anim = GetComponent<Animator>();   // Get the Animator component
    }

    void Update()
    {
        // Get horizontal movement input (A/D or Arrow keys)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Set the player's velocity (X axis movement and current Y velocity for jump/gravity)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip the sprite based on movement direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            isIdle = false; // Player is not idle if moving
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
            isIdle = false; // Player is not idle if jumping
        }

        // Update Animator parameters to play correct animations
        anim.SetBool("isRunning", moveInput != 0);  // isRunning when moving left or right
        anim.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0); // isJumping when jumping up
        anim.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < 0); // isFalling when falling down

        // Update the idle animation state
        if (isGrounded && moveInput == 0)
        {
            isIdle = true;
        }

        anim.SetBool("isIdle", isIdle); // Update isIdle bool in the Animator
    }

    void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
