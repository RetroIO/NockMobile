using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupPlayermove : MonoBehaviour 
{
    private float initialJumpPower = 4f;
    private float diminishingJumpFactor = 0.9f;
    private int availableJumps = 3;
    private int remainingJumps;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Reference to the Joystick component
    public Joystick joystick;
    public GameObject arrowPrefab;
    public float acceleration = 8f; // Adjust as needed
    public float deceleration = 3f; // Adjust as needed
	

    private bool isGrounded;
    private bool canJump = true;
    private int tapCount = 0;
    private float lastTapTime = 0f;
    private const float doubleTapTimeThreshold = 0.3f; // Adjust as needed

    private void Start()
    {
        remainingJumps = availableJumps;
    }

    private void Update()
    {
        // Get joystick.Horizontal for player movement
        float horizontal = joystick.Horizontal;

        if (IsLeftHalfOfScreenTapped())
        {
            HandleDoubleTapJump();
        }

        Flip(horizontal);
    }

    private void FixedUpdate()
    {
        // Use joystick.Horizontal for player movement
        float horizontal = joystick.Horizontal;

        // Apply gradual acceleration and deceleration
        if (horizontal != 0)
        {
            // Accelerate the player
            rb.velocity = new Vector2(rb.velocity.x + (horizontal * acceleration * Time.fixedDeltaTime), rb.velocity.y);
        }
        else
        {
            // Decelerate the player
            rb.velocity = new Vector2(rb.velocity.x - Mathf.Sign(rb.velocity.x) * deceleration * Time.fixedDeltaTime, rb.velocity.y);
        }

        // ... (rest of the code)
    }

    private bool IsLeftHalfOfScreenTapped()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
        {
            return true;
        }
    }
	

    return false;
}

    private void HandleDoubleTapJump()
    {
        if (Time.time - lastTapTime < doubleTapTimeThreshold)
        {
            tapCount++;
        }
        else
        {
            tapCount = 1;
        }

        lastTapTime = Time.time;

        if (tapCount == 2 && canJump)
        {
            Jump();
            tapCount = 0; // Reset tap count after successful double tap
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            float jumpPower = initialJumpPower;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        else if (remainingJumps > 0)
        {
            float jumpPower = initialJumpPower * Mathf.Pow(diminishingJumpFactor, availableJumps - remainingJumps);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        remainingJumps--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
        {
            remainingJumps = availableJumps;
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    private void Flip(float horizontal)
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
