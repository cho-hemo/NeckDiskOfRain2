using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterState
{
    protected MonsterFSM _fsm;
	protected Animator _anim;
    protected string _animTriggerName;

    public MonsterState(MonsterFSM fsm)
    {
        _fsm = fsm;
		_anim = fsm.GetComponent<Animator>();
        Enter();
    }

    public virtual void Enter()
    {

    }

    public virtual void Loop()
    {

    }

    public virtual void Exit()
    {

    }
}

public class MonsterMove : MonsterState
{
	public MonsterMove(MonsterFSM fsm) : base(fsm)
	{

	}

	public override void Enter()
    {
		base.Enter();
        _animTriggerName = "OnMove";
    }

	public override void Loop()
	{
		base.Loop();
		if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
		{
			_fsm.ChangeState(new MonsterAction(_fsm));
		}
	}
}

public class MonsterAction : MonsterState
{
    public MonsterAction(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
		base.Enter();
		_animTriggerName = "OnAction";
    }

    public override void Loop()
    {
		base.Loop();
	}
}

public class MonsterIdle : MonsterState
{
    public MonsterIdle(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
		base.Enter();
		_animTriggerName = "OnIdle";
    }

	public override void Loop()
	{
		base.Loop();
		if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrDetectRange)
		{
			_fsm.ChangeState(new MonsterMove(_fsm));
		}
	}
}

public class MonsterSpawn : MonsterState
{
    public MonsterSpawn(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
		base.Enter();
        _animTriggerName = "OnSpawn";
    }
}

public class MonsterDeath : MonsterState
{
    public MonsterDeath(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
		base.Enter();
		_animTriggerName = "OnDeath";
    }

    public override void Loop()
    {
		base.Loop();
	}

    public override void Exit()
    {
		base.Exit();
	}
}

public class MonsterAction1 : MonsterAction
{
    public MonsterAction1(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
}