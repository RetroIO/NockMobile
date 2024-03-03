using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallbackState : State
{
    // reference the state it needs to go to
    // any refences for calculations

    public PushingState pushingState;
    public MatchController matchController;

    public override State RunCurrentState()
    {
        Vector2 ballPosition = matchController.GetBallPosition();
        //anything here runs regardles and right away

        if (matchController.EnableAI == false) //first logic choice
        {
            return this; //return initialState
        }
        else if (ballPosition.x > transform.position.x)
        {

            //else if

            //return other states in logic - as a fail safe return a state at end

            return pushingState; //like this
        }
        return this;
    }
    
}
