using RiskOfRain2;
using RiskOfRain2.Manager;
using UnityEngine;

public class BeetleQueen : BossMonsterBase
{
    private Transform _spitSpawnPos;
    private Transform _beetleWardSpawnPos;

    private enum Skill
    {
        FIRE_SPIT = 0,
        FIRE_BEETLE,
        SUMMON_BEETLE
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool TrySelectSkill()
    {
        int currentIndex = 0;

        if (_coolDownTimes[(int)Skill.FIRE_SPIT] <= 0 && 
            _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_SPIT].SqrRange)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_SPIT;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.FIRE_BEETLE] <= 0 && 
            _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_BEETLE].SqrRange)// && Hp <= MaxHp / 2)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_BEETLE;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.SUMMON_BEETLE] <= 0 && 
            _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.SUMMON_BEETLE].SqrRange && 
            MonsterSpawner.IsSpawnable())
        {
            _availableSkills[currentIndex] = (int)Skill.SUMMON_BEETLE;
            ++currentIndex;
        }

        if (currentIndex > 0)
        {
            SkillNum = _availableSkills[Random.Range(0, currentIndex)];
            _anim.SetInteger("SkillNum", SkillNum);

            return true;
        }

        return false;
    }

    public override void ResetCoolDown()
    {
        if (SkillNum == -1 || SkillNum >= _coolDownTimes.Count)
            return;

        _coolDownTimes[SkillNum] = _skills[SkillNum].CoolDownTime;
        SkillNum = -1;
    }

    /// <summary>
    /// 침 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireSpit()
    {
        int spitCount = 6;
        float standardDegree = 150;

        for (int i = 0; i < spitCount; i++)
        {
            GameObject spit = ObjectPoolManager.Instance.ObjectPoolPop(Functions.POOL_BEETLE_QUEEN_SPIT);
            spit.transform.position = _spitSpawnPos.position;
            spit.transform.rotation = Quaternion.Euler(-30, _spitSpawnPos.eulerAngles.y + (standardDegree / 2) - (standardDegree / 5) * i, 0);
			spit.SetActive(true);
        }
    }

    /// <summary>
    /// 딱정벌레 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireBeetle()
    {
        GameObject beetleWard = ObjectPoolManager.Instance.ObjectPoolPop(Functions.POOL_BEETLE_QUEEN_BEETLE_WARD);
        beetleWard.transform.position = _beetleWardSpawnPos.position;
        beetleWard.transform.rotation = transform.rotation;
		beetleWard.SetActive(true);
    }

    /// <summary>
    /// 딱정벌레 소환 스킬 애니메이션 이벤트
    /// </summary>
    public void SummonBeetle()
    {
        Debug.Log("SkillNum2");

        //GameObject inst1 = Instantiate(
        //    _beetle,
        //    new Vector3(transform.position.x - 5, transform.position.y, transform.position.z),
        //    transform.rotation);

        //GameObject inst2 = Instantiate(
        //    _beetle,
        //    new Vector3(transform.position.x + 5, transform.position.y, transform.position.z),
        //    transform.rotation);
    }

    protected override void Awake()
    {
        base.Awake();
        _spitSpawnPos = gameObject.FindChildObject(Functions.BEETLE_QUEEN_SPIT_SPAWN_POS).transform;
        _beetleWardSpawnPos = gameObject.FindChildObject(Functions.BEETLE_QUEEN_BEETLE_WARD_SPAWN_POS).transform;
    }
}