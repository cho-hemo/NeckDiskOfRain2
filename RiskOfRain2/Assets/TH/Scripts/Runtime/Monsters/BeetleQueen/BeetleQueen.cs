using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Monster1
{
    [SerializeField] private GameObject _spit;
    [SerializeField] private Transform _spitStartPos;
    [SerializeField] private int _spitCount = 6;
    [SerializeField] private float _spitDegree = 150;

    [SerializeField] private float _fireSpitCoolDown;





    [SerializeField] private GameObject _beetleGrub;
    [SerializeField] private Transform _beetleGrubSpawnPos;
    [SerializeField] private float _fireBeetleCoolDown;



    [SerializeField] private GameObject _beetle;
    [SerializeField] private float _summonBeetleCoolDown;

    [SerializeField] private int[] _availableSkills = new int[3];

    private enum Skill
    {
        FIRE_SPIT = 0,
        FIRE_BEETLE,
        SUMMON_BEETLE
    }


    public void SelectSkill()
    {
        int currentIndex = 0;
        int skillNum;

        if (_fireSpitCoolDown <= 0)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_SPIT;
            ++currentIndex;
        }
        if (_fireBeetleCoolDown <= 0 && Hp <= MaxHp / 2)
        {
            _availableSkills[currentIndex] = (int)Skill.FIRE_BEETLE;
            ++currentIndex;
        }
        if (_summonBeetleCoolDown <= 0)
        {
            _availableSkills[currentIndex] = (int)Skill.SUMMON_BEETLE;
            ++currentIndex;
        }

        if (currentIndex > 0)
        {
            skillNum = Random.Range(0, currentIndex);
            GetComponent<Animator>().SetInteger("SkillNum", skillNum);
        }

		return ;
    }

    /// <summary>
    /// 침 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireSpit()
    {
        for (int i = 0; i < _spitCount; i++)
        {
            GameObject inst = Instantiate(
                _spit,
                _spitStartPos.position,
                Quaternion.Euler(-30, _spitStartPos.eulerAngles.y + (_spitDegree / 2) - (_spitDegree / 5) * i, 0));
        }
    }

    /// <summary>
    /// 딱정벌레 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireBeetle()
    {
        GameObject inst1 = Instantiate(
            _beetleGrub,
            _beetleGrubSpawnPos.position,
            transform.rotation);
    }

    /// <summary>
    /// 딱정벌레 소환 스킬 애니메이션
    /// </summary>
    public void SummonBeetle()
    {
        GameObject inst1 = Instantiate(
            _beetle,
            new Vector3(transform.position.x - 5, transform.position.y, transform.position.z),
            transform.rotation);

        GameObject inst2 = Instantiate(
            _beetle,
            new Vector3(transform.position.x + 5, transform.position.y, transform.position.z),
            transform.rotation);
    }
}