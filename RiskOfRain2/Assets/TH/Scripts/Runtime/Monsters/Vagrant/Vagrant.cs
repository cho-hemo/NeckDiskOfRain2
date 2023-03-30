using System.Collections.Generic;
using UnityEngine;

public class Vagrant : MonsterBase
{
    [SerializeField] private GameObject _superNovaHitArea;

    [SerializeField] private GameObject _trackingBomb;
    [SerializeField] private Transform _bombSpawnPos;

    [SerializeField] private GameObject _orb;
    [SerializeField] private Transform _orbSpawnPos;

    private enum Skill
    {
        SUPER_NOVA = 0,
        FIRE_TRACKING_BOMB,
        FIRE_ORB
    }

    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < _skills.Count; i++)
        {
            _coolDownTimes.Add(_skills[i].CoolDownTime);
        }
    }

    public override bool TrySelectSkill()
    {
        int currentIndex = 0;

        if (_coolDownTimes[(int)Skill.SUPER_NOVA] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.SUPER_NOVA].SqrRange)// && Hp <= MaxHp / 4)
        {
            _availableSkills[currentIndex] = (int)Skill.SUPER_NOVA;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.FIRE_TRACKING_BOMB] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_TRACKING_BOMB].SqrRange)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_TRACKING_BOMB;
            ++currentIndex;
        }
        if (_coolDownTimes[(int)Skill.FIRE_ORB] <= 0 && _fsm.GetSqrDistanceToPlayer() <= _skills[(int)Skill.FIRE_ORB].SqrRange)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_ORB;
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
    /// 슈퍼 노바 스킬 범위 생성 애니메이션 이벤트
    /// </summary>
    public void OnSuperNovaHitArea()
    {
        _superNovaHitArea.SetActive(true);
    }

    /// <summary>
    /// 슈퍼 노바 스킬
    /// </summary>
    public void OnSuperNova(float radius)
    {
		//애니메이션 재생
		_anim.SetTrigger("OnPostNova");
		if (_fsm.GetSqrDistanceToPlayer() <= radius * radius)//&& ray mask(ground, player)
		{
        //데미지 계산
		}
    }

    /// <summary>
    /// 추적 폭탄 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireTrackingBomb()
    {
        Instantiate(
            _trackingBomb,
            _bombSpawnPos.position,
            transform.rotation);
    }

    /// <summary>
    /// 구체 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireOrb()
    {
        Instantiate(
            _orb,
            _orbSpawnPos.position,
            transform.rotation);
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