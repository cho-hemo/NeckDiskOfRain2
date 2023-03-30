using UnityEngine;

public abstract class MonsterState
{
	protected MonsterBase _monster;
    protected MonsterFSM _fsm;
    protected Animator _anim;

    //
    protected string _animTriggerName;

    public MonsterState(MonsterBase monster, MonsterFSM fsm)
    {
		_monster = monster;
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
    public MonsterIdle(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
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

        if (_monster.TrySelectSkill())
        //if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
        {
            _fsm.ChangeState(_monster.OnSkillState);
        }
        else if (_fsm.GetSqrDistanceToPlayer() <= _monster.MaxSqrDetectRange && 
			_fsm.GetSqrDistanceToPlayer() > _monster.MinSqrDetectRange)
        {
            _fsm.ChangeState(_monster.MoveState);
        }
    }
}

public class MonsterMove : MonsterState
{
    private const float MOVE_COOL_DOWN_TIME = 1f;

    private float _timer;
    private RootMotion _rootMotion;

    public MonsterMove(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {
        _rootMotion = _fsm.GetComponent<RootMotion>();
    }

    public override void Enter()
    {
        base.Enter();
        _timer = 0f;
        _rootMotion.InitMove();
        _anim.SetTrigger(Functions.MONSTER_ANIM_MOVE);
    }

    public override void Loop()
    {
        base.Loop();

        if (_monster.TrySelectSkill())
        //if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SqrMaxAttackRange)
        {
            _fsm.ChangeState(_monster.OnSkillState);
        }
		else if(_fsm.GetSqrDistanceToPlayer() <= _monster.MinSqrDetectRange)
		{
			_fsm.ChangeState(_monster.IdleState);
		}
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _rootMotion.Move();
                _timer = MOVE_COOL_DOWN_TIME;
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
    public MonsterOnSkill(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //_fsm.LookAtPlayer();
        _anim.SetTrigger(Functions.MONSTER_ANIM_ON_SKILL);
    }

    public override void Loop()
    {
        base.Loop();
        if (_fsm.IsAnimationEnd)
        {
            _fsm.ChangeState(_monster.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _fsm.GetComponent<MonsterBase>().ResetCoolDown();

    }
}

public class MonsterSpawn : MonsterState
{
    public MonsterSpawn(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
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
            _fsm.ChangeState(_monster.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class MonsterDeath : MonsterState
{
    public MonsterDeath(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
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
    public MonsterOnDamaged(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
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
            _fsm.ChangeState(_monster.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class MonsterAction1 : MonsterOnSkill
{
    public MonsterAction1(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
}