using UnityEngine;

public abstract class MonsterState
{
    protected BossMonsterBase _boss;
    protected MonsterFSM _fsm;
    protected Animator _anim;

    public MonsterState(BossMonsterBase boss, MonsterFSM fsm)
    {
        _boss = boss;
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
    public MonsterIdle(BossMonsterBase boss, MonsterFSM fsm) : base(boss, fsm)
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

        if (_boss.TrySelectSkill())
        {
            _fsm.ChangeState(_boss.OnSkillState);
        }
        else if (_fsm.GetSqrDistanceToPlayer() <= _boss.MaxSqrDetectRange && 
            _fsm.GetSqrDistanceToPlayer() > _boss.MinSqrDetectRange)
        {
            _fsm.ChangeState(_boss.MoveState);
        }
    }
}

public class MonsterMove : MonsterState
{
    private const float MOVE_COOL_DOWN_TIME = 1f;

    private float _timer;
    private RootMotion _rootMotion;

    public MonsterMove(BossMonsterBase boss, MonsterFSM fsm) : base(boss, fsm)
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

        if (_boss.TrySelectSkill())
        {
            _fsm.ChangeState(_boss.OnSkillState);
        }
        else if(_fsm.GetSqrDistanceToPlayer() <= _boss.MinSqrDetectRange)
        {
            _fsm.ChangeState(_boss.IdleState);
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
    public MonsterOnSkill(BossMonsterBase boss, MonsterFSM fsm) : base(boss, fsm)
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
            _fsm.ChangeState(_boss.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _fsm.GetComponent<BossMonsterBase>().ResetCoolDown();

    }
}

public class MonsterSpawn : MonsterState
{
    public MonsterSpawn(BossMonsterBase boss, MonsterFSM fsm) : base(boss, fsm)
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
            _fsm.ChangeState(_boss.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class MonsterDeath : MonsterState
{
    public MonsterDeath(BossMonsterBase boss, MonsterFSM fsm) : base(boss, fsm)
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
			//사망
		}
	}

    public override void Exit()
    {
        base.Exit();
    }
}