using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingState : State
{
	public PushingState pushingState;
	public IdleState idleState;
	public DefendingState defendingState;
	public MatchController matchController;
	public float desiredDistanceToBall = 4f;
    public float movementSpeed = 3f;
    public float tooCloseDistance = 1.5f;
	public AIController aiController;
	
	public Rigidbody2D rb;
	
	public override State RunCurrentState()
	{
        if (matchController.EnableAI == false)
        {
            return this;
        }

        Vector2 ballPosition = matchController.GetBallPosition();
        Vector2 directionToBall = (ballPosition - (Vector2)transform.position).normalized;
        float distanceToBall = Vector2.Distance(transform.position, ballPosition);
		
		if (distanceToBall > desiredDistanceToBall)
		{
			Debug.Log("Moving toward ball");
            aiController.fireInterval = Random.Range(0.8f, 1.2f);
            rb.velocity = directionToBall * movementSpeed;
		}
		else if (distanceToBall <= desiredDistanceToBall - 0.5f)
		{
			Debug.Log("Going to idle state");
			return idleState;
		}
		
		return this;
		
	}
	
}


