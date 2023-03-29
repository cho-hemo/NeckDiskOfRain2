using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : MonsterBase
{

    [SerializeField] private GameObject _spit;
    [SerializeField] private Transform _spitStartPos;

    [SerializeField] private GameObject _beetleGrub;
    [SerializeField] private Transform _beetleGrubSpawnPos;

    [SerializeField] private GameObject _beetle;

    [SerializeField] private List<float> _coolDownTimes = new List<float>();
    [SerializeField] private int[] _availableSkills;

    private enum Skill
    {
        FIRE_SPIT = 0,
        FIRE_BEETLE,
        SUMMON_BEETLE
    }

    public override void Initialize(MonsterData data)
    {
        base.Initialize(data);
        for (int i = 0; i < _skills.Count; i++)
        {
            _coolDownTimes.Add(_skills[i].CoolDownTime);
        }
        _availableSkills = new int[_skills.Count];
    }

    public override bool TrySelectSkill()
    {
        int currentIndex = 0;

        if (_coolDownTimes[(int)Skill.FIRE_SPIT] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_SPIT].SqrRange)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_SPIT;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.FIRE_BEETLE] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_BEETLE].SqrRange)// && Hp <= MaxHp / 2)
		{
            _availableSkills[currentIndex] = (int)Skill.FIRE_BEETLE;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.SUMMON_BEETLE] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.SUMMON_BEETLE].SqrRange)
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
            GameObject inst = Instantiate(
                _spit,
                _spitStartPos.position,
                Quaternion.Euler(-30, _spitStartPos.eulerAngles.y + (standardDegree / 2) - (standardDegree / 5) * i, 0));
        }
    }

    /// <summary>
    /// 딱정벌레 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireBeetle()
    {
		GameObject inst = Instantiate(
			_beetleGrub,
			_beetleGrubSpawnPos.position,
			transform.rotation);
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

    private void Update()
    {
        for (int i = 0; i < _coolDownTimes.Count; i++)
        {
            if (_coolDownTimes[i] > 0)
            {
                _coolDownTimes[i] -= Time.deltaTime;
            }
        }
    }
}