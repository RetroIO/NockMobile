using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
	public bool canSeeThePlayer;
	public PushingState pushingState;
	public DefendingState defendingState;
	public FallbackState fallbackState;
	public MatchController matchController;
	public float desiredDistanceToBall = 3f;
    public float movementSpeed = 3f;
	public Rigidbody2D rb;
	public float tooCloseDistance = 1f;
	public AIController aiController;


    public override State RunCurrentState()
	{
		Vector2 ballPosition = matchController.GetBallPosition();
		Vector2 directionToBall = (ballPosition - (Vector2)transform.position).normalized;
		float distanceToBall = Vector2.Distance(transform.position, ballPosition);
		
		rb.velocity = Vector2.zero;
		aiController.fireInterval = 1.0f;
		Debug.Log("im staying here");			
		
		if (distanceToBall < tooCloseDistance + 1.5f)
		{
            Vector2 directionAwayFromBall = -directionToBall; // Calculate direction away from the ball
            rb.velocity = directionAwayFromBall * movementSpeed;
            Debug.Log("im backing off");
			return this;
		}
        else if (ballPosition.x > transform.position.x) // Check if ball is further positive on the X axis than the AI
        {
			// Return a new state here, such as a custom state for handling the ball being ahead of the AI
			Debug.Log("Ball has passed me");
            return defendingState; // Replace with the appropriate state class
        }
        else
        {
            Debug.Log("idle state last return");
            return pushingState;
        }
    }
}


