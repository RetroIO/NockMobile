using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : State
{
    public PushingState pushingState;
    public MatchController matchController;

    public override State RunCurrentState()
    {
        if (matchController.EnableAI == false)
        {
            return this;
        }
        Debug.Log("Starting state machine");
        return pushingState;
    }
}
