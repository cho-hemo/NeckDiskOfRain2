using UnityEngine;

public abstract class MonsterState
{
    protected MonsterFSM _fsm;
    protected Animator _anim;

    //
    protected string _animTriggerName;

    public MonsterState(MonsterFSM fsm)
    {
        _fsm = fsm;
        _anim = fsm.GetComponent<Animator>();
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
        _anim.SetTrigger(Functions.MONSTER_ANIM_IDLE);
    }

    public override void Loop()
    {
        base.Loop();

        if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
        {
            _fsm.ChangeState(new MonsterOnSkill(_fsm));
        }
        else if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrDetectRange)
        {
            _fsm.ChangeState(new MonsterMove(_fsm));
        }
    }
}

public class MonsterMove : MonsterState
{
	private const float COOL_DOWN_TIME = 1f;

	private float _timer;
    private RootMotion _rootMotion;

    public MonsterMove(MonsterFSM fsm) : base(fsm)
    {
		_timer = 0f;
        _rootMotion = _fsm.GetComponent<RootMotion>();
        _rootMotion.InitMove();
    }

    public override void Enter()
    {
        base.Enter();
        _anim.SetTrigger(Functions.MONSTER_ANIM_MOVE);
		//_rootMotion.Move();
	}

    public override void Loop()
    {
        base.Loop();
		if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
		{
			_fsm.ChangeState(new MonsterOnSkill(_fsm));
		}
		else
		{
			_timer -= Time.deltaTime;
			if (_timer <= 0f)
			{
				_rootMotion.Move();
				_timer = COOL_DOWN_TIME;
			}
		}
    }

    public override void Exit()
    {
        base.Exit();
        _rootMotion.Stop();
    }
}

public class MonsterOnSkill : MonsterState
{
    public MonsterOnSkill(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _anim.SetTrigger(Functions.MONSTER_ANIM_ON_SKILL);
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
        _anim.SetTrigger(Functions.MONSTER_ANIM_SPAWM);
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
        _anim.SetTrigger(Functions.MONSTER_ANIM_DEATH);
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

public class MonsterOnDamaged : MonsterState
{
    public MonsterOnDamaged(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _anim.SetTrigger(Functions.MONSTER_ANIM_DEATH);
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

public class MonsterAction1 : MonsterOnSkill
{
    public MonsterAction1(MonsterFSM fsm) : base(fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
}