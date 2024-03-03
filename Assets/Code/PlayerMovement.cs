using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    private float initialJumpPower = 5f;
    private float diminishingJumpFactor = 0.9f;
    private int availableJumps = 3;
    private int remainingJumps;
    private bool isFacingRight = true;
	private float lastJoystickJumpTime;
	private const float joystickJumpCooldown = 0.5f; // Adjust the cooldown duration as needed
	public Animator animator;
    public float maxSpeed = 5f;

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
		lastJoystickJumpTime = -1f; // Initialize to a negative value to ensure the first joystick jump is allowed
    }

    private void Update()
    {
        // Get joystick.Horizontal for player movement
        float horizontal = joystick.Horizontal;
		// Retrieve the selected jump mode from PlayerPrefs
		int jumpMode = PlayerPrefs.GetInt("JumpMode"); // Default to "Double Tap"
                                                       //float vertical = joystick.Vertical;
                                                       //Debug.Log(vertical);

        // Calculate movement speed from Rigidbody's velocity
        float speed = rb.velocity.magnitude;

        // Set the "Speed" parameter in the Animator
        animator.SetFloat("Speed", speed);

        if (IsLeftHalfOfScreenTapped() && jumpMode == 0)
        {
            HandleDoubleTapJump();
        }
		else if (VerticalCheck() && jumpMode == 1)
		{
			Debug.Log("Jumped");
			JoystickJump();
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

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
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
		// Retrieve the selected jump mode from PlayerPrefs
		int jumpMode = PlayerPrefs.GetInt("JumpMode"); // Default to "Double Tap"
		
		if (Time.time - lastTapTime < doubleTapTimeThreshold)
		{
			tapCount++;
		}
		else
		{
			tapCount = 1;
		}

		lastTapTime = Time.time;
		
		Debug.Log("Tap count: " + tapCount + "Double jump" + ", JumpMode: " + jumpMode);

		if (tapCount == 2 && canJump)
		{
			NormalJump();
			tapCount = 0; // Reset tap count after successful double tap
		}
	}

	private void JoystickJump()
	{
		// Calculate the time since the last joystick jump
		float timeSinceLastJoystickJump = Time.time - lastJoystickJumpTime;

		// Check if the cooldown period has passed
		if (timeSinceLastJoystickJump >= joystickJumpCooldown)
		{
			// Mark the current time as the last joystick jump time
			lastJoystickJumpTime = Time.time;

			if (canJump)
			{
				NormalJump();
			}
		}
	}
	
	private bool VerticalCheck()
	{
		float vertical = joystick.Vertical;
		float jumpThreshold = 0.4f; // Adjust the threshold as needed
		if (vertical > jumpThreshold)
		{
			return true;
		}
		return false;
	}
	
	private void NormalJump()
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
		Debug.Log(remainingJumps);
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
