using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

	public State currentState;
	public State initialState;

    // Update is called once per frame
    void Update()
    {
		RunStateMachine();
    }
	
	private void RunStateMachine()
	{
		
		State nextState = currentState?.RunCurrentState();
		
		if (nextState !=null)
		{
			SwitchToNextState(nextState);
		}
	}
	
	private void SwitchToNextState(State nextState)
	{
		currentState = nextState;
	}

    public void SetCurrentState(State newState)
    {
        currentState = newState;
    }

    public void ResetToInitialState()
    {
      
        SwitchToNextState(initialState);
    }

}
