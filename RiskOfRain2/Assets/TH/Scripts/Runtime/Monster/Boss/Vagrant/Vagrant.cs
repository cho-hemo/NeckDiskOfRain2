using RiskOfRain2;
using RiskOfRain2.Manager;
using UnityEngine;
using VagrantSkill;

public class Vagrant : BossMonsterBase
{
    private GameObject _superNovaHitArea;
    private Transform _projectileSpawnPos;

    private GameObject _player;
    private LayerMask _mask;

    private enum Skill
    {
        SUPER_NOVA = 0,
        FIRE_TRACKING_BOMB,
        FIRE_ORB
    }

    public override void Initialize()
    {
        base.Initialize();

        _player = GameManager.Instance.Player.gameObject;
        _mask = LayerMask.GetMask(Functions.LAYER_GROUND) | LayerMask.GetMask(Functions.LAYER_PLAYER);
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
        _anim.SetTrigger("OnPostNova");
        if (_fsm.GetSqrDistanceToPlayer() <= radius * radius)
        {
            Ray ray = new Ray(transform.position, (_player.transform.position - transform.position).normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, radius, _mask))
            {
                //데미지
            }
        }
    }

    /// <summary>
    /// 추적 폭탄 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireTrackingBomb()
    {
        TrackingBomb trackingBomb = ObjectPoolManager.Instance.ObjectPoolPop(Functions.POOL_VAGRANT_TRACKING_BOMB).GetComponent<TrackingBomb>();
		trackingBomb.SetStats(Power);
        trackingBomb.transform.position = _projectileSpawnPos.position;
        trackingBomb.transform.rotation = transform.rotation;
		trackingBomb.gameObject.SetActive(true);
    }

    /// <summary>
    /// 구체 발사 스킬 애니메이션 이벤트
    /// </summary>
    public void FireOrb()
    {
        Orb orb = ObjectPoolManager.Instance.ObjectPoolPop(Functions.POOL_VAGRANT_ORB).GetComponent<Orb>();
		orb.SetStats(Power);
		orb.transform.position = _projectileSpawnPos.position;
        orb.transform.rotation = transform.rotation;
		orb.gameObject.SetActive(true);
    }

    protected override void Awake()
    {
        base.Awake();
        _superNovaHitArea = gameObject.FindChildObject(Functions.VAGRANT_SUPER_NOVA_HIT_AREA);
        _projectileSpawnPos = gameObject.FindChildObject(Functions.VAGRANT_PROJECTILE_SPAWN_POS).transform;
    }
}