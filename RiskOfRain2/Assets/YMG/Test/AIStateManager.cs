using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
	public AIState currentState;

	private void Start()
	{
		
	}

	void Update()
    {
		RunStateMachine();

	}

	private void RunStateMachine() 
	{
		AIState nextState = currentState?.RunCurrentState();

		if (nextState != null) 
		{
			// switch to the next state
			SwitchToTheNextState(nextState);
		}
	}

	private void SwitchToTheNextState(AIState nextState) 
	{
		currentState = nextState;
	}
}
