using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStateMachine : MonoBehaviour
{
	MobBaseState currentState;

    void Start()
    {
		currentState = GetInitialState();
    }

    void Update()
    {
        if (currentState != null) 
		{
			currentState.UpdateLogic();
		}
    }

	void LateUpdate()
	{
		if (currentState != null)
		{
			currentState.UpdatePhysics();
		}
	}

	public void ChangeState(MobBaseState newState) 
	{
		currentState.Exit();
		currentState = newState;
		currentState.Enter();
	}

	protected virtual MobBaseState GetInitialState() 
	{
		return null;
	}
}
