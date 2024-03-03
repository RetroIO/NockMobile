using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingState : State
{
	public IdleState idleState;
	public MatchController matchController;
	public PushingState pushingState;
	public float desiredDistanceToBall = 3f;
    public float movementSpeed = 3f;
	public Vector2 defendingSpot = new Vector2(10.0f, 0.6f);
	public Rigidbody2D rb;
	public AIController aiController;
	
	public override State RunCurrentState()
	{
		
		Vector2 ballPosition = matchController.GetBallPosition();
        Vector2 directionToBall = (ballPosition - (Vector2)transform.position).normalized;
        float distanceToBall = Vector2.Distance(transform.position, ballPosition);
		Vector2 directionToDefendingSpot = (defendingSpot - (Vector2)transform.position).normalized;
		
		Debug.Log("Im going to defend");
        aiController.fireInterval = 2.0f;
        rb.velocity = directionToDefendingSpot * movementSpeed * 1.5f;

        if (transform.position.x >= ballPosition.x + 2f)
        {
            Debug.Log("I can push again");
            return pushingState;
        }

        if (Vector2.Distance(transform.position, defendingSpot) < 1f)
			{
				Debug.Log("I have reached the dfeending spot");
				return idleState;
			}
			
		return this;
	}
		

	
}
