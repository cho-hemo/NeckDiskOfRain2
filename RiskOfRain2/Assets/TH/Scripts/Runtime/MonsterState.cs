using System;
using UnityEngine;

public abstract class MonsterState
{
    protected const string ANIM_SPAWM = "OnSpawn";
    protected const string ANIM_IDLE = "OnIdle";
    protected const string ANIM_MOVE = "OnMove";
    protected const string ANIM_ACTION = "OnAction";
    protected const string ANIM_DEATH = "OnDeath";

    protected MonsterFSM _fsm;
    protected Animator _anim;

    //
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

public class MonsterIdle : MonsterState
{
	public MonsterIdle(MonsterFSM fsm) : base(fsm)
	{

	}

	public override void Enter()
	{
		base.Enter();
		_anim.SetTrigger(ANIM_IDLE);
	}

	public override void Loop()
	{
		base.Loop();

		if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
		{
			_fsm.ChangeState(new MonsterAction(_fsm));
		}
		else if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrDetectRange)
		{
			_fsm.ChangeState(new MonsterMove(_fsm));
		}
	}
}

public class MonsterMove : MonsterState
{
    private RootMotion _rootMotion;

    public MonsterMove(MonsterFSM fsm) : base(fsm)
    {
        _rootMotion = _fsm.GetComponent<RootMotion>();
        _rootMotion.InitMove();
    }

    public override void Enter()
    {
        base.Enter();
        _anim.SetTrigger(ANIM_MOVE);
    }

    public override void Loop()
    {
		base.Loop();
        if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
        {
			Debug.Log("Change Attack");
            _fsm.ChangeState(new MonsterAction(_fsm));
        }
        else
        {
            _rootMotion.Move();
        }
    }

    public override void Exit()
    {
        base.Exit();
        _rootMotion.Stop();
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
        _anim.SetTrigger(ANIM_ACTION);
    }

    public override void Loop()
    {
        base.Loop();
        if (_fsm.IsAnimationEnd)
        {
            _fsm.ChangeState(new MonsterIdle(_fsm));
        }
    }

    public override void Exit()
    {
        base.Exit();
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
        _anim.SetTrigger(ANIM_SPAWM);
    }

    public override void Loop()
    {
        base.Loop();
        if (_fsm.IsAnimationEnd)
        {
            _fsm.ChangeState(new MonsterIdle(_fsm));
        }
    }

    public override void Exit()
    {
        base.Exit();
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
        _anim.SetTrigger(ANIM_DEATH);
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