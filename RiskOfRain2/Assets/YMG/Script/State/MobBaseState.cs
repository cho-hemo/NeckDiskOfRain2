using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBaseState
{
	public string name;
	protected MobStateMachine stateMachine;

	public MobBaseState(string name, MobStateMachine mobStateMachine)
	{
		this.name = name;
		this.stateMachine = mobStateMachine;
	}

	public virtual void Enter() 
	{ 

	}
	public virtual void UpdateLogic() 
	{ 

	}
	public virtual void UpdatePhysics() 
	{ 

	}
	public virtual void Exit() 
	{ 

	}
}
