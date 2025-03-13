using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isRunning;
    private bool isJumping;
    private bool isGrounded; // To check if the player is on the ground

    private float moveInput;

    public Transform groundCheck; // Reference to the ground check position
    public float groundCheckRadius = 0.2f; // Radius for the ground check
    public LayerMask groundLayer; // Layer that defines the ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground check using a small circle cast at the player's feet
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get player input for movement (A and D keys)
        moveInput = 0;
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1; // Move right
        }

        // Check if the player is trying to jump (only if grounded) - Space key
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
        }

        // Keep `isJumping` true while the player is in the air
        if (!isGrounded && rb.linearVelocity.y > 0) // Player is still jumping upwards
        {
            isJumping = true;
        }
        else if (isGrounded) // Player is grounded
        {
            isJumping = false;
        }

        // Update running animation based on movement input
        if (moveInput != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Update the animator parameters
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);

        // Move the player horizontally
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip character sprite based on movement direction
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 1, 1); // Keep facing the same direction if not moving
        }

        // Check if the player has fallen below y = -8
        if (transform.position.y < -8)
        {
            // If in the Unity editor, stop playing the game
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            // If a built game, quit the application
            Application.Quit();
            #endif
        }
    }
}
